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
using RefundRequest = Data.Models.RefundRequest;

namespace Service.RefundRequestsService
{
    public class RefundRequestService : IRefundRequestService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public RefundRequestService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<RefundResponse>> GetAll()
        {

            var list = await _unitOfWork.Repository<Data.Models.RefundRequest>().GetAll()
                                            .ProjectTo<RefundResponse>(_mapper.ConfigurationProvider)
                                            .ToListAsync();
            return list;
        }

        public async Task<RefundResponse> Get(Guid id)
        {
            try
            {
                RefundRequest refund = await _unitOfWork.Repository<RefundRequest>()
                .GetAll()
                .AsNoTracking()
                .Include(x => x.Enrollment)
                    .ThenInclude(x => x.Transaction)
                        .ThenInclude(x => x.Course)
                .Include(x => x.Enrollment)
                    .ThenInclude(x => x.Transaction)
                        .ThenInclude(x => x.Learner)
                            .ThenInclude(x => x.Account)
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();


                if (refund == null)
                {
                    throw new Exception("khong tim thay");
                }

                return _mapper.Map<RefundRequest, RefundResponse>(refund);
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<RefundResponse> Create(ViewModel.RequestModel.RefundRequest request)
        {
            // Set the UTC offset for UTC+7
            TimeSpan utcOffset = TimeSpan.FromHours(7);

            // Get the current UTC time
            DateTime utcNow = DateTime.UtcNow;

            // Convert the UTC time to UTC+7
            DateTime localTime = utcNow + utcOffset;
            try
            {
                var refund = _mapper.Map<ViewModel.RequestModel.RefundRequest, Data.Models.RefundRequest>(request);
                refund.Id = Guid.NewGuid();
                refund.RequestedDate = localTime;
                refund.Status = "PROCESSING";
                await _unitOfWork.Repository<Data.Models.RefundRequest>().InsertAsync(refund);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<Data.Models.RefundRequest, RefundResponse>(refund);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<RefundResponse> Delete(Guid id)
        {
            try
            {
                Data.Models.RefundRequest refund = null;
                refund = _unitOfWork.Repository<Data.Models.RefundRequest>()
                    .Find(p => p.Id == id);
                if (refund == null)
                {
                    throw new Exception("Bi trung id");
                }
                await _unitOfWork.Repository<Center>().HardDeleteGuid(refund.Id);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<Data.Models.RefundRequest, RefundResponse>(refund);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<RefundResponse> Update(Guid id, ViewModel.RequestModel.RefundRequest request)
        {
            // Set the UTC offset for UTC+7
            TimeSpan utcOffset = TimeSpan.FromHours(7);

            // Get the current UTC time
            DateTime utcNow = DateTime.UtcNow;

            // Convert the UTC time to UTC+7
            DateTime localTime = utcNow + utcOffset;
            try
            {
                Data.Models.RefundRequest refund = _unitOfWork.Repository<Data.Models.RefundRequest>()
                            .Find(x => x.Id == id);
                if (refund == null)
                {
                    throw new Exception();
                }
                // Update the refund request based on the incoming request
                refund = _mapper.Map(request, refund);

                // Update ApprovedDate based on Status
                if (refund.Status == "APPROVED" || refund.Status == "DISAPPROVED")
                {
                    refund.ApprovedDate = refund.Status == "APPROVED" ? localTime : null;
                }

                await _unitOfWork.Repository<Data.Models.RefundRequest>().UpdateDetached(refund);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<Data.Models.RefundRequest, RefundResponse>(refund);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<RefundHistoryResponse>> GetRefundHistoryByRefundRequest(Guid id)
        {
            try
            {
                var refundHistories = await _unitOfWork.Repository<RefundHistory>().GetAll()
                     .Include(x => x.RefundRequest)
                    .Where(x => x.RefundRequestId == id)
                    .ProjectTo<RefundHistoryResponse>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                if (refundHistories == null)
                {
                    throw new Exception("khong tim thay");
                }

                return refundHistories;
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

    }
}
