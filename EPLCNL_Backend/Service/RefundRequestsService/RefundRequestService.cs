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

        public async Task<RefundResponse> Create(ViewModel.RequestModel.RefundRequest request)
        {
            try
            {
                var refund = _mapper.Map<ViewModel.RequestModel.RefundRequest, Data.Models.RefundRequest>(request);
                refund.Id = Guid.NewGuid();
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
            try
            {
                Data.Models.RefundRequest refund = _unitOfWork.Repository<Data.Models.RefundRequest>()
                            .Find(x => x.Id == id);
                if (refund == null)
                {
                    throw new Exception();
                }
                refund = _mapper.Map(request, refund);

                await _unitOfWork.Repository<Data.Models.RefundRequest>().UpdateDetached(refund);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<Data.Models.RefundRequest, RefundResponse>(refund);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

       
    }
}
