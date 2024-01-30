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

namespace Service.QuizzesService
{
    public class QuizService : IQuizService 
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public QuizService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<QuizResponse>> GetAll()
        {

            var list = await _unitOfWork.Repository<Quiz>().GetAll()
                                            .ProjectTo<QuizResponse>(_mapper.ConfigurationProvider)
                                            .ToListAsync();
            return list;
        }

        public async Task<QuizResponse> Get(Guid id)
        {
            try
            {
                Quiz quiz = null;
                quiz = await _unitOfWork.Repository<Quiz>().GetAll()
                     .AsNoTracking()
                     .Include(a => a.Module)
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();

                if (quiz == null)
                {
                    throw new Exception("khong tim thay");
                }

                return _mapper.Map<Quiz, QuizResponse>(quiz);
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        public async Task<QuizResponse> Create(QuizRequest request)
        {
            // Set the UTC offset for UTC+7
            TimeSpan utcOffset = TimeSpan.FromHours(7);

            // Get the current UTC time
            DateTime utcNow = DateTime.UtcNow;

            // Convert the UTC time to UTC+7
            DateTime localTime = utcNow + utcOffset;
            try
            {
                var quiz = _mapper.Map<QuizRequest, Quiz>(request);
                quiz.Id = Guid.NewGuid();
                quiz.CreatedDate = localTime;
                await _unitOfWork.Repository<Quiz>().InsertAsync(quiz);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<Quiz, QuizResponse>(quiz);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<QuizResponse> Delete(Guid id)
        {
            try
            {
                Quiz quiz = null;
                quiz = _unitOfWork.Repository<Quiz>()
                    .Find(p => p.Id == id);
                if (quiz == null)
                {
                    throw new Exception("Bi trung id");
                }
                await _unitOfWork.Repository<Quiz>().HardDeleteGuid(quiz.Id);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<Quiz, QuizResponse>(quiz);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<QuizResponse> Update(Guid id, QuizRequest request)
        {
            // Set the UTC offset for UTC+7
            TimeSpan utcOffset = TimeSpan.FromHours(7);

            // Get the current UTC time
            DateTime utcNow = DateTime.UtcNow;

            // Convert the UTC time to UTC+7
            DateTime localTime = utcNow + utcOffset;
            try
            {
                Quiz quiz = _unitOfWork.Repository<Quiz>()
                            .Find(x => x.Id == id);
                if (quiz == null)
                {
                    throw new Exception();
                }
                quiz = _mapper.Map(request, quiz);
                quiz.UpdatedDate = localTime;
                await _unitOfWork.Repository<Quiz>().UpdateDetached(quiz);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<Quiz, QuizResponse>(quiz);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
