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

namespace Service.ClassTypesService
{
    public class ClassTypeService : IClassTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public ClassTypeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ClassTypeResponse>> GetAll()
        {

            var list = await _unitOfWork.Repository<ClassType>().GetAll()
                                            .ProjectTo<ClassTypeResponse>(_mapper.ConfigurationProvider)
                                            .ToListAsync();
            return list;
        }

        public async Task<ClassTypeResponse> Get(Guid id)
        {
            try
            {
                ClassType classType = null;
                classType = await _unitOfWork.Repository<ClassType>().GetAll()
                     .AsNoTracking()
                     .Include(x => x.Course)
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();

                if (classType == null)
                {
                    throw new Exception("khong tim thay");
                }

                return _mapper.Map<ClassType, ClassTypeResponse>(classType);
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<ClassTypeResponse> Create(ClassTypeRequest request)
        {
            try
            {
                var classType = _mapper.Map<ClassTypeRequest, ClassType>(request);
                classType.Id = Guid.NewGuid();
                await _unitOfWork.Repository<ClassType>().InsertAsync(classType);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<ClassType, ClassTypeResponse>(classType);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<ClassTypeResponse> Delete(Guid id)
        {
            try
            {
                ClassType classType = null;
                classType = _unitOfWork.Repository<ClassType>()
                    .Find(p => p.Id == id);
                if (classType == null)
                {
                    throw new Exception("Bi trung id");
                }
                await _unitOfWork.Repository<ClassType>().HardDeleteGuid(classType.Id);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<ClassType, ClassTypeResponse>(classType);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ClassTypeResponse> Update(Guid id, ClassTypeRequest request)
        {
            try
            {
                ClassType classType = _unitOfWork.Repository<ClassType>()
                            .Find(x => x.Id == id);
                if (classType == null)
                {
                    throw new Exception();
                }
                classType = _mapper.Map(request, classType);

                await _unitOfWork.Repository<ClassType>().UpdateDetached(classType);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<ClassType, ClassTypeResponse>(classType);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
