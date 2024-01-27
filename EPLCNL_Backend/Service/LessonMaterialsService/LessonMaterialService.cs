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

namespace Service.LessonMaterialsService
{
    public class LessonMaterialService : ILessonMaterialService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public LessonMaterialService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<LessonMaterialResponse>> GetAll()
        {

            var list = await _unitOfWork.Repository<LessonMaterial>().GetAll()
                                            .ProjectTo<LessonMaterialResponse>(_mapper.ConfigurationProvider)
                                            .ToListAsync();
            return list;
        }

        public async Task<LessonMaterialResponse> Get(Guid id)
        {
            try
            {
                LessonMaterial lessonMaterial = null;
                lessonMaterial = await _unitOfWork.Repository<LessonMaterial>().GetAll()
                     .AsNoTracking()
                     .Include(x => x.Lesson)
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();

                if (lessonMaterial == null)
                {
                    throw new Exception("khong tim thay");
                }

                return _mapper.Map<LessonMaterial, LessonMaterialResponse>(lessonMaterial);
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<LessonMaterialResponse> Create(LessonMaterialRequest request)
        {
            try
            {
                var lessonMaterial = _mapper.Map<LessonMaterialRequest, LessonMaterial>(request);
                lessonMaterial.Id = Guid.NewGuid();
                await _unitOfWork.Repository<LessonMaterial>().InsertAsync(lessonMaterial);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<LessonMaterial, LessonMaterialResponse>(lessonMaterial);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<LessonMaterialResponse> Delete(Guid id)
        {
            try
            {
                LessonMaterial lessonMaterial = null;
                lessonMaterial = _unitOfWork.Repository<LessonMaterial>()
                    .Find(p => p.Id == id);
                if (lessonMaterial == null)
                {
                    throw new Exception("Bi trung id");
                }
                await _unitOfWork.Repository<LessonMaterial>().HardDeleteGuid(lessonMaterial.Id);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<LessonMaterial, LessonMaterialResponse>(lessonMaterial);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<LessonMaterialResponse> Update(Guid id, LessonMaterialRequest request)
        {
            try
            {
                LessonMaterial lessonMaterial = _unitOfWork.Repository<LessonMaterial>()
                            .Find(x => x.Id == id);
                if (lessonMaterial == null)
                {
                    throw new Exception();
                }
                lessonMaterial = _mapper.Map(request, lessonMaterial);

                await _unitOfWork.Repository<LessonMaterial>().UpdateDetached(lessonMaterial);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<LessonMaterial, LessonMaterialResponse>(lessonMaterial);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
