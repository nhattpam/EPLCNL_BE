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

namespace Service.ClassTopicsService
{
    public class ClassTopicService : IClassTopicService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public ClassTopicService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ClassTopicResponse>> GetAll()
        {

            var list = await _unitOfWork.Repository<ClassTopic>().GetAll()
                                            .ProjectTo<ClassTopicResponse>(_mapper.ConfigurationProvider)
                                            .ToListAsync();
            return list;
        }

        public async Task<ClassTopicResponse> Get(Guid id)
        {
            try
            {
                ClassTopic classTopic = null;
                classTopic = await _unitOfWork.Repository<ClassTopic>().GetAll()
                     .AsNoTracking()
                     .Include(x => x.ClassLesson)
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();

                if (classTopic == null)
                {
                    throw new Exception("khong tim thay");
                }

                return _mapper.Map<ClassTopic, ClassTopicResponse>(classTopic);
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<LessonMaterialResponse>> GetAllMaterialsByClassTopic(Guid id)
        {
            var classTopic = await _unitOfWork.Repository<ClassTopic>().GetAll()
                 .Where(x => x.Id == id)
                 .FirstOrDefaultAsync();

            if (classTopic == null)
            {
                // Handle the case where the center with the specified id is not found
                return null;
            }

            var materials = _unitOfWork.Repository<LessonMaterial>().GetAll()
                .Where(t => t.ClassTopicId == id)
                .ProjectTo<LessonMaterialResponse>(_mapper.ConfigurationProvider)
                .ToList();

            return materials;
        }

        public async Task<ClassTopicResponse> Create(ClassTopicRequest request)
        {
            // Set the UTC offset for UTC+7
            TimeSpan utcOffset = TimeSpan.FromHours(7);

            // Get the current UTC time
            DateTime utcNow = DateTime.UtcNow;

            // Convert the UTC time to UTC+7
            DateTime localTime = utcNow + utcOffset;
            try
            {
                var classTopic = _mapper.Map<ClassTopicRequest, ClassTopic>(request);
                classTopic.Id = Guid.NewGuid();
                classTopic.CreatedDate = localTime;
                await _unitOfWork.Repository<ClassTopic>().InsertAsync(classTopic);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<ClassTopic, ClassTopicResponse>(classTopic);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<ClassTopicResponse> Delete(Guid id)
        {
            try
            {
                ClassTopic classTopic = null;
                classTopic = _unitOfWork.Repository<ClassTopic>()
                    .Find(p => p.Id == id);
                if (classTopic == null)
                {
                    throw new Exception("Bi trung id");
                }
                await _unitOfWork.Repository<ClassTopic>().HardDeleteGuid(classTopic.Id);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<ClassTopic, ClassTopicResponse>(classTopic);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ClassTopicResponse> Update(Guid id, ClassTopicRequest request)
        {
            // Set the UTC offset for UTC+7
            TimeSpan utcOffset = TimeSpan.FromHours(7);

            // Get the current UTC time
            DateTime utcNow = DateTime.UtcNow;

            // Convert the UTC time to UTC+7
            DateTime localTime = utcNow + utcOffset;
            try
            {
                ClassTopic classTopic = _unitOfWork.Repository<ClassTopic>()
                            .Find(x => x.Id == id);
                if (classTopic == null)
                {
                    throw new Exception();
                }
                classTopic = _mapper.Map(request, classTopic);
                classTopic.UpdatedDate = localTime;

                await _unitOfWork.Repository<ClassTopic>().UpdateDetached(classTopic);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<ClassTopic, ClassTopicResponse>(classTopic);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<List<QuizResponse>> GetAllQuizzesByClassTopic(Guid id)
        {
            var classTopic = await _unitOfWork.Repository<ClassTopic>().GetAll()
               .Where(x => x.Id == id)
               .FirstOrDefaultAsync();

            if (classTopic == null)
            {
                // Handle the case where the center with the specified id is not found
                return null;
            }

            var quizzes = _unitOfWork.Repository<Quiz>().GetAll()
                .Where(t => t.ClassTopicId == id)
                .ProjectTo<QuizResponse>(_mapper.ConfigurationProvider)
                .ToList();

            return quizzes;
        }
    }
}
