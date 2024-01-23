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

namespace Service.ClassMaterialsService
{
    public class ClassMaterialService : IClassMaterialService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public ClassMaterialService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ClassMaterialResponse>> GetClassMaterials()
        {

            var list = await _unitOfWork.Repository<ClassMaterial>().GetAll()
                                            .ProjectTo<ClassMaterialResponse>(_mapper.ConfigurationProvider)
                                            .ToListAsync();
            return list;
        }

        public async Task<ClassMaterialResponse> Create(ClassMaterialRequest request)
        {
            try
            {
                var classMaterial = _mapper.Map<ClassMaterialRequest, ClassMaterial>(request);
                classMaterial.Id = Guid.NewGuid();
                await _unitOfWork.Repository<ClassMaterial>().InsertAsync(classMaterial);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<ClassMaterial, ClassMaterialResponse>(classMaterial);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<ClassMaterialResponse> Delete(Guid id)
        {
            try
            {
                ClassMaterial classMaterial = null;
                classMaterial = _unitOfWork.Repository<ClassMaterial>()
                    .Find(p => p.Id == id);
                if (classMaterial == null)
                {
                    throw new Exception("Bi trung id");
                }
                await _unitOfWork.Repository<ClassMaterial>().HardDeleteGuid(classMaterial.Id);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<ClassMaterial, ClassMaterialResponse>(classMaterial);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ClassMaterialResponse> Update(Guid id, ClassMaterialRequest request)
        {
            try
            {
                ClassMaterial classMaterial = _unitOfWork.Repository<ClassMaterial>()
                            .Find(x => x.Id == id);
                if (classMaterial == null)
                {
                    throw new Exception();
                }
                classMaterial = _mapper.Map(request, classMaterial);

                await _unitOfWork.Repository<ClassMaterial>().UpdateDetached(classMaterial);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<ClassMaterial, ClassMaterialResponse>(classMaterial);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
