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

namespace Service.ClassLessonsService
{
    public class ClassLessonService : IClassLessonService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public ClassLessonService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ClassLessonResponse>> GetAll()
        {

            var list = await _unitOfWork.Repository<ClassLesson>().GetAll()
                                            .ProjectTo<ClassLessonResponse>(_mapper.ConfigurationProvider)
                                            .ToListAsync();
            return list;
        }

        public async Task<ClassLessonResponse> Get(Guid id)
        {
            try
            {
                ClassLesson classLesson = null;
                classLesson = await _unitOfWork.Repository<ClassLesson>().GetAll()
                     .AsNoTracking()
                     .Include(x => x.ClassModule)
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();

                if (classLesson == null)
                {
                    throw new Exception("khong tim thay");
                }

                return _mapper.Map<ClassLesson, ClassLessonResponse>(classLesson);
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<ClassTopicResponse>> GetAllClassTopicsByClassLesson(Guid id)
        {
            var classLesson = await _unitOfWork.Repository<ClassLesson>().GetAll()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (classLesson == null)
            {
                // Handle the case where the center with the specified id is not found
                return null;
            }

            var classTopics = _unitOfWork.Repository<ClassTopic>().GetAll()
                .Where(t => t.ClassLessonId == id)
                .ProjectTo<ClassTopicResponse>(_mapper.ConfigurationProvider)
                .ToList();

            return classTopics;
        }


        public async Task<ClassLessonResponse> Create(ClassLessonRequest request)
        {
            try
            {
                var classLesson = _mapper.Map<ClassLessonRequest, ClassLesson>(request);
                classLesson.Id = Guid.NewGuid();
                await _unitOfWork.Repository<ClassLesson>().InsertAsync(classLesson);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<ClassLesson, ClassLessonResponse>(classLesson);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<ClassLessonResponse> Delete(Guid id)
        {
            try
            {
                ClassLesson classLesson = null;
                classLesson = _unitOfWork.Repository<ClassLesson>()
                    .Find(p => p.Id == id);
                if (classLesson == null)
                {
                    throw new Exception("Bi trung id");
                }
                await _unitOfWork.Repository<ClassLesson>().HardDeleteGuid(classLesson.Id);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<ClassLesson, ClassLessonResponse>(classLesson);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ClassLessonResponse> Update(Guid id, ClassLessonRequest request)
        {
            try
            {
                ClassLesson classLesson = _unitOfWork.Repository<ClassLesson>()
                            .Find(x => x.Id == id);
                if (classLesson == null)
                {
                    throw new Exception();
                }
                classLesson = _mapper.Map(request, classLesson);

                await _unitOfWork.Repository<ClassLesson>().UpdateDetached(classLesson);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<ClassLesson, ClassLessonResponse>(classLesson);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
