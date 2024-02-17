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

namespace Service.ViolationsService
{
    public class ViolationService: IViolationService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public ViolationService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<ViolationResponse>> GetAll()
        {

            var list = await _unitOfWork.Repository<Violation>().GetAll()
                                            .ProjectTo<ViolationResponse>(_mapper.ConfigurationProvider)
                                            .ToListAsync();
            return list;
        }

        public async Task<ViolationResponse> Get(Guid id)
        {
            try
            {
                Violation violation = null;
                violation = await _unitOfWork.Repository<Violation>().GetAll()
                     .AsNoTracking()
                     .Include(a => a.Course)
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();

                if (violation == null)
                {
                    throw new Exception("khong tim thay");
                }

                return _mapper.Map<Violation, ViolationResponse>(violation);
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public async Task<ViolationResponse> Create(ViolationRequest request)
        {
            // Set the UTC offset for UTC+7
            TimeSpan utcOffset = TimeSpan.FromHours(7);

            // Get the current UTC time
            DateTime utcNow = DateTime.UtcNow;

            // Convert the UTC time to UTC+7
            DateTime localTime = utcNow + utcOffset;
            try
            {
                var violation = _mapper.Map<ViolationRequest, Violation>(request);
                violation.Id = Guid.NewGuid();
                violation.ViolatedDate = localTime;
                await _unitOfWork.Repository<Violation>().InsertAsync(violation);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<Violation, ViolationResponse>(violation);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<ViolationResponse> Delete(Guid id)
        {
            try
            {
                Violation violation = null;
                violation = _unitOfWork.Repository<Violation>()
                    .Find(p => p.Id == id);
                if (violation == null)
                {
                    throw new Exception("Bi trung id");
                }
                await _unitOfWork.Repository<Violation>().HardDeleteGuid(violation.Id);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<Violation, ViolationResponse>(violation);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ViolationResponse> Update(Guid id, ViolationRequest request)
        {
            // Set the UTC offset for UTC+7
            TimeSpan utcOffset = TimeSpan.FromHours(7);

            // Get the current UTC time
            DateTime utcNow = DateTime.UtcNow;

            // Convert the UTC time to UTC+7
            DateTime localTime = utcNow + utcOffset;
            try
            {
                Violation violation = _unitOfWork.Repository<Violation>()
                            .Find(x => x.Id == id);
                if (violation == null)
                {
                    throw new Exception();
                }
                violation = _mapper.Map(request, violation);
                violation.ViolatedDate = localTime;
                await _unitOfWork.Repository<Violation>().UpdateDetached(violation);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<Violation, ViolationResponse>(violation);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
