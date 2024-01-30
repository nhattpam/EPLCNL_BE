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

namespace Service.LessonsService
{
    public class LessonService : ILessonService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public LessonService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<LessonResponse>> GetAll()
        {

            var list = await _unitOfWork.Repository<Lesson>().GetAll()
                                            .ProjectTo<LessonResponse>(_mapper.ConfigurationProvider)
                                            .ToListAsync();
            return list;
        }

        public async Task<LessonResponse> Get(Guid id)
        {
            try
            {
                Lesson lesson = null;
                lesson = await _unitOfWork.Repository<Lesson>().GetAll()
                     .AsNoTracking()
                     .Include(x => x.Module)
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();

                if (lesson == null)
                {
                    throw new Exception("khong tim thay");
                }

                return _mapper.Map<Lesson, LessonResponse>(lesson);
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<LessonMaterialResponse>> GetAllMaterialsByLesson(Guid id)
        {
            var lesson = await _unitOfWork.Repository<Lesson>().GetAll()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (lesson == null)
            {
                // Handle the case where the center with the specified id is not found
                return null;
            }

            var materials = _unitOfWork.Repository<LessonMaterial>().GetAll()
                .Where(t => t.LessonId == id)
                .ProjectTo<LessonMaterialResponse>(_mapper.ConfigurationProvider)
                .ToList();

            return materials;
        }

        public async Task<LessonResponse> Create(LessonRequest request)
        {
            // Set the UTC offset for UTC+7
            TimeSpan utcOffset = TimeSpan.FromHours(7);

            // Get the current UTC time
            DateTime utcNow = DateTime.UtcNow;

            // Convert the UTC time to UTC+7
            DateTime localTime = utcNow + utcOffset;
            try
            {
                var lesson = _mapper.Map<LessonRequest, Lesson>(request);
                lesson.Id = Guid.NewGuid();
                lesson.CreatedDate = localTime;
                await _unitOfWork.Repository<Lesson>().InsertAsync(lesson);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<Lesson, LessonResponse>(lesson);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<LessonResponse> Delete(Guid id)
        {
            try
            {
                Lesson lesson = null;
                lesson = _unitOfWork.Repository<Lesson>()
                    .Find(p => p.Id == id);
                if (lesson == null)
                {
                    throw new Exception("Bi trung id");
                }
                await _unitOfWork.Repository<Lesson>().HardDeleteGuid(lesson.Id);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<Lesson, LessonResponse>(lesson);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<LessonResponse> Update(Guid id, LessonRequest request)
        {
            // Set the UTC offset for UTC+7
            TimeSpan utcOffset = TimeSpan.FromHours(7);

            // Get the current UTC time
            DateTime utcNow = DateTime.UtcNow;

            // Convert the UTC time to UTC+7
            DateTime localTime = utcNow + utcOffset;
            try
            {
                Lesson lesson = _unitOfWork.Repository<Lesson>()
                            .Find(x => x.Id == id);
                if (lesson == null)
                {
                    throw new Exception();
                }
                lesson = _mapper.Map(request, lesson);
                lesson.UpdatedDate = localTime;

                await _unitOfWork.Repository<Lesson>().UpdateDetached(lesson);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<Lesson, LessonResponse>(lesson);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        
    }
}
