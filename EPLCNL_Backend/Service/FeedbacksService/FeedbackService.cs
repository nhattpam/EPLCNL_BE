using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Models;
using Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace Service.FeedbacksService
{
    public class FeedbackService : IFeedbackService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public FeedbackService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<FeedbackResponse>> GetAll()
        {

            var list = await _unitOfWork.Repository<Feedback>().GetAll()
                                            .ProjectTo<FeedbackResponse>(_mapper.ConfigurationProvider)
                                            .ToListAsync();
            return list;
        }

        public async Task<FeedbackResponse> Get(Guid id)
        {
            try
            {
                Feedback feedback = null;
                feedback = await _unitOfWork.Repository<Feedback>().GetAll()
                     .AsNoTracking()
                     .Include(x => x.Course)
                     .Include(x => x.Learner)
                     .ThenInclude(x => x.Account)
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();

                if (feedback == null)
                {
                    throw new Exception("khong tim thay");
                }

                return _mapper.Map<Feedback, FeedbackResponse>(feedback);
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<FeedbackResponse> Create(FeedbackRequest request)
        {
            // Set the UTC offset for UTC+7
            TimeSpan utcOffset = TimeSpan.FromHours(7);

            // Get the current UTC time
            DateTime utcNow = DateTime.UtcNow;

            // Convert the UTC time to UTC+7
            DateTime localTime = utcNow + utcOffset;
            try
            {
                var feedback = _mapper.Map<FeedbackRequest, Feedback>(request);
                feedback.Id = Guid.NewGuid();
                feedback.CreatedDate = localTime;
                await _unitOfWork.Repository<Feedback>().InsertAsync(feedback);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<Feedback, FeedbackResponse>(feedback);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<FeedbackResponse> Delete(Guid id)
        {
            try
            {
                Feedback feedback = null;
                feedback = _unitOfWork.Repository<Feedback>()
                    .Find(p => p.Id == id);
                if (feedback == null)
                {
                    throw new Exception("Bi trung id");
                }
                await _unitOfWork.Repository<Feedback>().HardDeleteGuid(feedback.Id);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<Feedback, FeedbackResponse>(feedback);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<FeedbackResponse> Update(Guid id, FeedbackRequest request)
        {
            try
            {
                Feedback feedback = _unitOfWork.Repository<Feedback>()
                            .Find(x => x.Id == id);
                if (feedback == null)
                {
                    throw new Exception();
                }
                feedback = _mapper.Map(request, feedback);

                await _unitOfWork.Repository<Feedback>().UpdateDetached(feedback);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<Feedback, FeedbackResponse>(feedback);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
