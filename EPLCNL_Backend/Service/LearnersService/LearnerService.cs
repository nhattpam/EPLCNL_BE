using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Models;
using Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Service.EnrollmentsService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;
using RefundRequest = Data.Models.RefundRequest;

namespace Service.LearnersService
{
    public class LearnerService : ILearnerService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private IEnrollmentService _enrollmentService;
        public LearnerService(IUnitOfWork unitOfWork, IMapper mapper, IEnrollmentService enrollmentService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _enrollmentService = enrollmentService;
        }

        public async Task<List<LearnerResponse>> GetAll()
        {

            var list = await _unitOfWork.Repository<Learner>().GetAll()
                                            .ProjectTo<LearnerResponse>(_mapper.ConfigurationProvider)
                                            .ToListAsync();
            return list;
        }

        public async Task<LearnerResponse> Get(Guid? id)
        {
            try
            {
                Learner learner = null;
                learner = await _unitOfWork.Repository<Learner>().GetAll()
                     .AsNoTracking()
                     .Include(x => x.Account)
                     .ThenInclude(x => x.Wallet)
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();

                if (learner == null)
                {
                    throw new Exception("khong tim thay");
                }

                return _mapper.Map<Learner, LearnerResponse>(learner);
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<LearnerResponse> Create(LearnerRequest request)
        {
            try
            {
                var learner = _mapper.Map<LearnerRequest, Learner>(request);
                learner.Id = Guid.NewGuid();
                await _unitOfWork.Repository<Learner>().InsertAsync(learner);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<Learner, LearnerResponse>(learner);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<LearnerResponse> Delete(Guid id)
        {
            try
            {
                Learner learner = null;
                learner = _unitOfWork.Repository<Learner>()
                    .Find(p => p.Id == id);
                if (learner == null)
                {
                    throw new Exception("Bi trung id");
                }
                await _unitOfWork.Repository<Learner>().HardDeleteGuid(learner.Id);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<Learner, LearnerResponse>(learner);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<LearnerResponse> Update(Guid id, LearnerRequest request)
        {
            try
            {
                Learner learner = _unitOfWork.Repository<Learner>()
                            .Find(x => x.Id == id);
                if (learner == null)
                {
                    throw new Exception();
                }
                learner = _mapper.Map(request, learner);

                await _unitOfWork.Repository<Learner>().UpdateDetached(learner);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<Learner, LearnerResponse>(learner);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public async Task<List<ForumResponse>> GetAllForumsByLearner(Guid id)
        {
            var learner = await _unitOfWork.Repository<Learner>()
                .GetAll()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (learner == null)
            {
                throw new Exception($"Learner with id {id} not found.");
            }

            var enrollments = await _unitOfWork.Repository<Enrollment>()
                .GetAll()
                .Where(t => t.Transaction.LearnerId == id && t.RefundStatus == false)
                .Include(x => x.Transaction)
                .ToListAsync();

            var forumResponses = new List<ForumResponse>(); // Move this outside the loop

            foreach (var enrollment in enrollments)
            {
                var forums = await _unitOfWork.Repository<Forum>()
                    .GetAll()
                    .Where(forum => forum.CourseId == enrollment.Transaction.CourseId)
                    .ProjectTo<ForumResponse>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                forumResponses.AddRange(forums);
            }

            return forumResponses; // Move this outside the loop
        }




        public async Task<List<EnrollmentResponse>> GetAllEnrollmentsByLearner(Guid id)
        {
            // Retrieve the learner
            var learner = await _unitOfWork.Repository<Learner>().GetAll()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (learner == null)
            {
                // Handle the case where the learner with the specified id is not found
                return null;
            }

            // Retrieve enrollments for the learner
            var enrollments = await _unitOfWork.Repository<Enrollment>().GetAll()
                .Where(t => t.Transaction.LearnerId == id && t.RefundStatus == false)
                 .Include(x => x.Transaction)
                .ProjectTo<EnrollmentResponse>(_mapper.ConfigurationProvider)
                                            .ToListAsync();

            return enrollments;
        }

        public async Task<List<TransactionResponse>> GetAllTransactionsByLearner(Guid id)
        {
            try
            {
                var transactions = await _unitOfWork.Repository<Transaction>().GetAll()
                     .AsNoTracking()
                     .Include(x => x.PaymentMethod)
                     .Include(x => x.Learner)
                     .ThenInclude(x => x.Account)
                     .Include(x => x.Course)
                    .Where(x => x.LearnerId == id)
                    .ProjectTo<TransactionResponse>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                if (transactions.Count == 0)
                {
                    throw new Exception("khong tim thay");
                }

                return transactions;
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<AssignmentAttemptResponse>> GetAllAssignmentAttemptsByLearner(Guid learnerId)
        {
            var learner = await _unitOfWork.Repository<Learner>().GetAll()
                .Where(x => x.Id == learnerId)
                .FirstOrDefaultAsync();

            if (learner == null)
            {
                // Handle the case where the tutor with the specified id is not found
                return null;
            }

            var assignmentAttempts = _unitOfWork.Repository<AssignmentAttempt>().GetAll()
                .Where(a => a.LearnerId == learnerId)
                .ProjectTo<AssignmentAttemptResponse>(_mapper.ConfigurationProvider)
                .ToList();

            return assignmentAttempts;
        }

        public async Task<List<QuizAttemptResponse>> GetAllQuizAttemptsByLearner(Guid learnerId)
        {
            var learner = await _unitOfWork.Repository<Learner>().GetAll()
                .Where(x => x.Id == learnerId)
                .FirstOrDefaultAsync();

            if (learner == null)
            {
                // Handle the case where the tutor with the specified id is not found
                return null;
            }

            var quizAttempts = _unitOfWork.Repository<QuizAttempt>().GetAll()
                .Where(a => a.LearnerId == learnerId)
                .ProjectTo<QuizAttemptResponse>(_mapper.ConfigurationProvider)
                .ToList();

            return quizAttempts;
        }
        public async Task<List<RefundResponse>> GetAllRefundsByLearner(Guid id)
        {
            try
            {
                var learner = await _unitOfWork.Repository<Learner>().GetAll()
               .Where(x => x.Id == id)
               .FirstOrDefaultAsync();

                if (learner == null)
                {
                    // Handle the case where the tutor with the specified id is not found
                    return null;
                }
                var refunds = await _unitOfWork.Repository<RefundRequest>().GetAll()
                     .AsNoTracking()
                    .ProjectTo<RefundResponse>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                var learnerRefunds = new List<RefundResponse>();

                foreach (var refund in refunds)
                {
                    var enrollment = await _enrollmentService.Get(refund.EnrollmentId ?? Guid.Empty);
                    if (enrollment.Transaction?.LearnerId == id)
                    {
                        learnerRefunds.Add(refund);
                    }
                }

                if (refunds.Count == 0)
                {
                    throw new Exception("khong tim thay");
                }

                return learnerRefunds;
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<ProfileCertificateResponse>> GetAllProfileCertificatesByLearner(Guid id)
        {
            var learner = await _unitOfWork.Repository<Learner>().GetAll()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (learner == null)
            {
                // Handle the case where the tutor with the specified id is not found
                return null;
            }

            var profileCertificates = _unitOfWork.Repository<ProfileCertificate>().GetAll()
                .Include(x => x.Certificate)
                    .ThenInclude(x => x.Course)
                .Include(x => x.Learner)
                .Where(a => a.LearnerId == id)
                .ProjectTo<ProfileCertificateResponse>(_mapper.ConfigurationProvider)
                .ToList();

            return profileCertificates;
        }
    }
}
