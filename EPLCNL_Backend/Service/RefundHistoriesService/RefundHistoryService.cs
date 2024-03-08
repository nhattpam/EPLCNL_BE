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

namespace Service.RefundHistoriesService
{
    public class RefundHistoryService: IRefundHistoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public RefundHistoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<RefundHistoryResponse>> GetAll()
        {

            var list = await _unitOfWork.Repository<RefundHistory>().GetAll()
                                            .ProjectTo<RefundHistoryResponse>(_mapper.ConfigurationProvider)
                                            .ToListAsync();
            return list;
        }

        public async Task<RefundHistoryResponse> Get(Guid id)
        {
            try
            {
                RefundHistory refundHistory = null;
                refundHistory = await _unitOfWork.Repository<RefundHistory>().GetAll()
                     .AsNoTracking()
                     .Include(x => x.RefundRequest)
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();

                if (refundHistory == null)
                {
                    throw new Exception("khong tim thay");
                }

                return _mapper.Map<RefundHistory, RefundHistoryResponse>(refundHistory);
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public async Task<RefundHistoryResponse> Create(RefundHistoryRequest request)
        {
            try
            {
                var refundHistory = _mapper.Map<RefundHistoryRequest, RefundHistory>(request);
                refundHistory.Id = Guid.NewGuid();
                await _unitOfWork.Repository<RefundHistory>().InsertAsync(refundHistory);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<RefundHistory, RefundHistoryResponse>(refundHistory);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<RefundHistoryResponse> Delete(Guid id)
        {
            try
            {
                RefundHistory refundHistory = null;
                refundHistory = _unitOfWork.Repository<RefundHistory>()
                    .Find(p => p.Id == id);
                if (refundHistory == null)
                {
                    throw new Exception("Bi trung id");
                }
                await _unitOfWork.Repository<RefundHistory>().HardDeleteGuid(refundHistory.Id);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<RefundHistory, RefundHistoryResponse>(refundHistory);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<RefundHistoryResponse> Update(Guid id, RefundHistoryRequest request)
        {
            try
            {
                RefundHistory refundHistory = _unitOfWork.Repository<RefundHistory>()
                            .Find(x => x.Id == id);
                if (refundHistory == null)
                {
                    throw new Exception();
                }
                refundHistory = _mapper.Map(request, refundHistory);

                await _unitOfWork.Repository<RefundHistory>().UpdateDetached(refundHistory);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<RefundHistory, RefundHistoryResponse>(refundHistory);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
