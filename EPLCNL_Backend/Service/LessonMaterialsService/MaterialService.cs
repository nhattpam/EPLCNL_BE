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

namespace Service.MaterialsService
{
    public class MaterialService : IMaterialService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public MaterialService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<MaterialResponse>> GetAll()
        {

            var list = await _unitOfWork.Repository<Material>().GetAll()
                                            .ProjectTo<MaterialResponse>(_mapper.ConfigurationProvider)
                                            .ToListAsync();
            return list;
        }

        public async Task<MaterialResponse> Get(Guid id)
        {
            try
            {
                Material material = null;
                material = await _unitOfWork.Repository<Material>().GetAll()
                     .AsNoTracking()
                     .Include(x => x.Lesson)
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();

                if (material == null)
                {
                    throw new Exception("khong tim thay");
                }

                return _mapper.Map<Material, MaterialResponse>(material);
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<MaterialResponse> Create(MaterialRequest request)
        {
            // Set the UTC offset for UTC+7
            TimeSpan utcOffset = TimeSpan.FromHours(7);

            // Get the current UTC time
            DateTime utcNow = DateTime.UtcNow;

            // Convert the UTC time to UTC+7
            DateTime localTime = utcNow + utcOffset;
            try
            {
                var material = _mapper.Map<MaterialRequest, Material>(request);
                material.Id = Guid.NewGuid();
                material.CreatedDate = localTime;
                await _unitOfWork.Repository<Material>().InsertAsync(material);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<Material, MaterialResponse>(material);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<MaterialResponse> Delete(Guid id)
        {
            try
            {
                Material material = null;
                material = _unitOfWork.Repository<Material>()
                    .Find(p => p.Id == id);
                if (material == null)
                {
                    throw new Exception("Bi trung id");
                }
                await _unitOfWork.Repository<Material>().HardDeleteGuid(material.Id);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<Material, MaterialResponse>(material);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<MaterialResponse> Update(Guid id, MaterialRequest request)
        {
            // Set the UTC offset for UTC+7
            TimeSpan utcOffset = TimeSpan.FromHours(7);

            // Get the current UTC time
            DateTime utcNow = DateTime.UtcNow;

            // Convert the UTC time to UTC+7
            DateTime localTime = utcNow + utcOffset;
            try
            {
                Material material = _unitOfWork.Repository<Material>()
                            .Find(x => x.Id == id);
                if (material == null)
                {
                    throw new Exception();
                }
                material = _mapper.Map(request, material);
                material.UpdatedDate = localTime;

                await _unitOfWork.Repository<Material>().UpdateDetached(material);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<Material, MaterialResponse>(material);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
