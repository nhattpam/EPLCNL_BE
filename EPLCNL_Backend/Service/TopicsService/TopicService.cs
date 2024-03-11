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

namespace Service.TopicsService
{
    public class TopicService : ITopicService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public TopicService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<TopicResponse>> GetAll()
        {

            var list = await _unitOfWork.Repository<Topic>().GetAll()
                                            .ProjectTo<TopicResponse>(_mapper.ConfigurationProvider)
                                            .ToListAsync();
            return list;
        }

        public async Task<TopicResponse> Get(Guid id)
        {
            try
            {
                Topic classTopic = null;
                classTopic = await _unitOfWork.Repository<Topic>().GetAll()
                     .AsNoTracking()
                     .Include(x => x.ClassLesson)
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();

                if (classTopic == null)
                {
                    throw new Exception("khong tim thay");
                }

                return _mapper.Map<Topic, TopicResponse>(classTopic);
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<MaterialResponse>> GetAllMaterialsByClassTopic(Guid id)
        {
            var classTopic = await _unitOfWork.Repository<Topic>().GetAll()
                 .Where(x => x.Id == id)
                 .FirstOrDefaultAsync();

            if (classTopic == null)
            {
                // Handle the case where the center with the specified id is not found
                return null;
            }

            var materials = _unitOfWork.Repository<Material>().GetAll()
                .Where(t => t.TopicId == id)
                .ProjectTo<MaterialResponse>(_mapper.ConfigurationProvider)
                .ToList();

            return materials;
        }

        public async Task<TopicResponse> Create(TopicRequest request)
        {
            // Set the UTC offset for UTC+7
            TimeSpan utcOffset = TimeSpan.FromHours(7);

            // Get the current UTC time
            DateTime utcNow = DateTime.UtcNow;

            // Convert the UTC time to UTC+7
            DateTime localTime = utcNow + utcOffset;
            try
            {
                var classTopic = _mapper.Map<TopicRequest, Topic>(request);
                classTopic.Id = Guid.NewGuid();
                classTopic.CreatedDate = localTime;
                await _unitOfWork.Repository<Topic>().InsertAsync(classTopic);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<Topic, TopicResponse>(classTopic);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<TopicResponse> Delete(Guid id)
        {
            try
            {
                Topic classTopic = null;
                classTopic = _unitOfWork.Repository<Topic>()
                    .Find(p => p.Id == id);
                if (classTopic == null)
                {
                    throw new Exception("Bi trung id");
                }
                await _unitOfWork.Repository<Topic>().HardDeleteGuid(classTopic.Id);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<Topic, TopicResponse>(classTopic);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<TopicResponse> Update(Guid id, TopicRequest request)
        {
            // Set the UTC offset for UTC+7
            TimeSpan utcOffset = TimeSpan.FromHours(7);

            // Get the current UTC time
            DateTime utcNow = DateTime.UtcNow;

            // Convert the UTC time to UTC+7
            DateTime localTime = utcNow + utcOffset;
            try
            {
                Topic classTopic = _unitOfWork.Repository<Topic>()
                            .Find(x => x.Id == id);
                if (classTopic == null)
                {
                    throw new Exception();
                }
                classTopic = _mapper.Map(request, classTopic);
                classTopic.UpdatedDate = localTime;

                await _unitOfWork.Repository<Topic>().UpdateDetached(classTopic);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<Topic, TopicResponse>(classTopic);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<QuizResponse>> GetAllQuizzesByClassTopic(Guid id)
        {
            var classTopic = await _unitOfWork.Repository<Topic>().GetAll()
               .Where(x => x.Id == id)
               .FirstOrDefaultAsync();

            if (classTopic == null)
            {
                // Handle the case where the center with the specified id is not found
                return null;
            }

            var quizzes = _unitOfWork.Repository<Quiz>().GetAll()
                .Where(t => t.TopicId == id)
                .ProjectTo<QuizResponse>(_mapper.ConfigurationProvider)
                .ToList();

            return quizzes;
        }
    }
}
