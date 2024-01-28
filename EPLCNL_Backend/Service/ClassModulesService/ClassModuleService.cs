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

namespace Service.ClassModulesService
{
    public class ClassModuleService : IClassModuleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public ClassModuleService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ClassModuleResponse>> GetAll()
        {

            var list = await _unitOfWork.Repository<ClassModule>().GetAll()
                                            .ProjectTo<ClassModuleResponse>(_mapper.ConfigurationProvider)
                                            .ToListAsync();
            return list;
        }

        public async Task<ClassModuleResponse> Get(Guid id)
        {
            try
            {
                ClassModule classModule = null;
                classModule = await _unitOfWork.Repository<ClassModule>().GetAll()
                     .AsNoTracking()
                        .Include(a => a.Course)
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();

                if (classModule == null)
                {
                    throw new Exception("khong tim thay");
                }

                return _mapper.Map<ClassModule, ClassModuleResponse>(classModule);
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<ClassLessonResponse>> GetAllClassLessonsByModule(Guid id)
        {
            var classModule = await _unitOfWork.Repository<ClassModule>().GetAll()
               .Where(x => x.Id == id)
               .FirstOrDefaultAsync();

            if (classModule == null)
            {
                // Handle the case where the center with the specified id is not found
                return null;
            }

            var classLessons = _unitOfWork.Repository<ClassLesson>().GetAll()
                .Where(t => t.ClassModuleId == id)
                .ProjectTo<ClassLessonResponse>(_mapper.ConfigurationProvider)
                .ToList();

            return classLessons;
        }

        public async Task<ClassModuleResponse> Create(ClassModuleRequest request)
        {
            try
            {
                var classModule = _mapper.Map<ClassModuleRequest, ClassModule>(request);
                classModule.Id = Guid.NewGuid();
                await _unitOfWork.Repository<ClassModule>().InsertAsync(classModule);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<ClassModule, ClassModuleResponse>(classModule);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<ClassModuleResponse> Delete(Guid id)
        {
            try
            {
                ClassModule classModule = null;
                classModule = _unitOfWork.Repository<ClassModule>()
                    .Find(p => p.Id == id);
                if (classModule == null)
                {
                    throw new Exception("Bi trung id");
                }
                await _unitOfWork.Repository<ClassModule>().HardDeleteGuid(classModule.Id);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<ClassModule, ClassModuleResponse>(classModule);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ClassModuleResponse> Update(Guid id, ClassModuleRequest request)
        {
            try
            {
                ClassModule classModule = _unitOfWork.Repository<ClassModule>()
                            .Find(x => x.Id == id);
                if (classModule == null)
                {
                    throw new Exception();
                }
                classModule = _mapper.Map(request, classModule);

                await _unitOfWork.Repository<ClassModule>().UpdateDetached(classModule);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<ClassModule, ClassModuleResponse>(classModule);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
