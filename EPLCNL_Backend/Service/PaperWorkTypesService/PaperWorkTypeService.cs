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

namespace Service.PaperWorkTypesService
{
    public class PaperWorkTypeService : IPaperWorkTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public PaperWorkTypeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<PaperWorkTypeResponse>> GetAll()
        {

            var list = await _unitOfWork.Repository<PaperWorkType>().GetAll()
                                            .ProjectTo<PaperWorkTypeResponse>(_mapper.ConfigurationProvider)
                                            .ToListAsync();
            return list;
        }

        public async Task<PaperWorkTypeResponse> Get(Guid id)
        {
            try
            {
                PaperWorkType paperWorkType = null;
                paperWorkType = await _unitOfWork.Repository<PaperWorkType>().GetAll()
                     .AsNoTracking()
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();

                if (paperWorkType == null)
                {
                    throw new Exception("khong tim thay");
                }

                return _mapper.Map<PaperWorkType, PaperWorkTypeResponse>(paperWorkType);
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<PaperWorkTypeResponse> Create(PaperWorkTypeRequest request)
        {
            try
            {
                var paperWorkType = _mapper.Map<PaperWorkTypeRequest, PaperWorkType>(request);
                paperWorkType.Id = Guid.NewGuid();
                await _unitOfWork.Repository<PaperWorkType>().InsertAsync(paperWorkType);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<PaperWorkType, PaperWorkTypeResponse>(paperWorkType);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<PaperWorkTypeResponse> Delete(Guid id)
        {
            try
            {
                PaperWorkType paperWorkType = null;
                paperWorkType = _unitOfWork.Repository<PaperWorkType>()
                    .Find(p => p.Id == id);
                if (paperWorkType == null)
                {
                    throw new Exception("Bi trung id");
                }
                await _unitOfWork.Repository<PaperWorkType>().HardDeleteGuid(paperWorkType.Id);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<PaperWorkType, PaperWorkTypeResponse>(paperWorkType);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<PaperWorkTypeResponse> Update(Guid id, PaperWorkTypeRequest request)
        {
            try
            {
                PaperWorkType paperWorkType = _unitOfWork.Repository<PaperWorkType>()
                            .Find(x => x.Id == id);
                if (paperWorkType == null)
                {
                    throw new Exception();
                }
                paperWorkType = _mapper.Map(request, paperWorkType);

                await _unitOfWork.Repository<PaperWorkType>().UpdateDetached(paperWorkType);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<PaperWorkType, PaperWorkTypeResponse>(paperWorkType);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
