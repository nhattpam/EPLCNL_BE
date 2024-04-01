using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Models;
using Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Service.AssignmentAttemptsService;
using Service.AssignmentsService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace Service.PeerReviewsService
{
    public class PeerReviewService : IPeerReviewService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private IAssignmentAttemptService _assignmentAttemptService;
        public PeerReviewService(IUnitOfWork unitOfWork, IMapper mapper, IAssignmentService assignmentService, IAssignmentAttemptService assignmentAttemptService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _assignmentAttemptService = assignmentAttemptService;
        }

        public async Task<List<PeerReviewResponse>> GetAll()
        {

            var list = await _unitOfWork.Repository<PeerReview>().GetAll()
                                            .ProjectTo<PeerReviewResponse>(_mapper.ConfigurationProvider)
                                            .ToListAsync();
            return list;
        }

        public async Task<PeerReviewResponse> Get(Guid id)
        {
            try
            {
                PeerReview peerReview = null;
                peerReview = await _unitOfWork.Repository<PeerReview>().GetAll()
                     .AsNoTracking()
                     .Include(x => x.Learner)
                     .Include(x => x.AssignmentAttempt)
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();

                if (peerReview == null)
                {
                    throw new Exception("khong tim thay");
                }

                return _mapper.Map<PeerReview, PeerReviewResponse>(peerReview);
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


       public async Task<PeerReviewResponse> Create(PeerReviewRequest request)
{
            try
            {
                var peerReview = _mapper.Map<PeerReviewRequest, PeerReview>(request);
                peerReview.Id = Guid.NewGuid();
                await _unitOfWork.Repository<PeerReview>().InsertAsync(peerReview);
                await _unitOfWork.CommitAsync();

                // Calculate sum grade and number of peers
                double? sumGrade = 0;
                int numberOfPeers = 0;
                var peerByAttempts = await _assignmentAttemptService.GetAllPeerReviewsByAssignmentAttempt(request.AssignmentAttemptId ?? Guid.Empty);
                foreach (var peer in peerByAttempts)
                {
                    sumGrade += peer.Grade;
                    numberOfPeers++;
                }

                // Update the assignment attempt with the calculated total grade
                var attemptResponse = await _assignmentAttemptService.Get(request.AssignmentAttemptId ?? Guid.Empty);
                var attemptRequest = new AssignmentAttemptRequest()
                {
                    AnswerText = attemptResponse.AnswerText,
                    LearnerId = attemptResponse.LearnerId,
                    AttemptedDate = attemptResponse.AttemptedDate,
                    AssignmentId = attemptResponse.AssignmentId,
                    TotalGrade = numberOfPeers > 0 ? sumGrade / numberOfPeers : null,
                };
                await _assignmentAttemptService.Update(attemptResponse.Id, attemptRequest);

                return _mapper.Map<PeerReview, PeerReviewResponse>(peerReview);
    }
    catch (Exception e)
    {
        throw new Exception(e.Message);
    }
}


        public async Task<PeerReviewResponse> Delete(Guid id)
        {
            try
            {
                PeerReview peerReview = null;
                peerReview = _unitOfWork.Repository<PeerReview>()
                    .Find(p => p.Id == id);
                if (peerReview == null)
                {
                    throw new Exception("Bi trung id");
                }
                await _unitOfWork.Repository<PeerReview>().HardDeleteGuid(peerReview.Id);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<PeerReview, PeerReviewResponse>(peerReview);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PeerReviewResponse> Update(Guid id, PeerReviewRequest request)
        {
           
            try
            {
                PeerReview peerReview = _unitOfWork.Repository<PeerReview>()
                            .Find(x => x.Id == id);
                if (peerReview == null)
                {
                    throw new Exception();
                }
                peerReview = _mapper.Map(request, peerReview);

                await _unitOfWork.Repository<PeerReview>().UpdateDetached(peerReview);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<PeerReview, PeerReviewResponse>(peerReview);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        
    }
}
