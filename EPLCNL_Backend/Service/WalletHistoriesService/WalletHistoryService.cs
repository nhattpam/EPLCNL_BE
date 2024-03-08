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

namespace Service.WalletHistoriesService
{
    public class WalletHistoryService: IWalletHistoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public WalletHistoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<WalletHistoryResponse>> GetAll()
        {

            var list = await _unitOfWork.Repository<WalletHistory>().GetAll()
                                            .ProjectTo<WalletHistoryResponse>(_mapper.ConfigurationProvider)
                                            .ToListAsync();
            return list;
        }

        public async Task<WalletHistoryResponse> Get(Guid id)
        {
            try
            {
                WalletHistory walletHistory = null;
                walletHistory = await _unitOfWork.Repository<WalletHistory>().GetAll()
                     .AsNoTracking()
                     .Include(x => x.Wallet)
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();

                if (walletHistory == null)
                {
                    throw new Exception("khong tim thay");
                }

                return _mapper.Map<WalletHistory, WalletHistoryResponse>(walletHistory);
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public async Task<WalletHistoryResponse> Create(WalletHistoryRequest request)
        {
            // Set the UTC offset for UTC+7
            TimeSpan utcOffset = TimeSpan.FromHours(7);

            // Get the current UTC time
            DateTime utcNow = DateTime.UtcNow;

            // Convert the UTC time to UTC+7
            DateTime localTime = utcNow + utcOffset;
            try
            {
                var walletHistory = _mapper.Map<WalletHistoryRequest, WalletHistory>(request);
                walletHistory.Id = Guid.NewGuid();
                walletHistory.TransactionDate = localTime;
                await _unitOfWork.Repository<WalletHistory>().InsertAsync(walletHistory);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<WalletHistory, WalletHistoryResponse>(walletHistory);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<WalletHistoryResponse> Delete(Guid id)
        {
            try
            {
                WalletHistory walletHistory = null;
                walletHistory = _unitOfWork.Repository<WalletHistory>()
                    .Find(p => p.Id == id);
                if (walletHistory == null)
                {
                    throw new Exception("Bi trung id");
                }
                await _unitOfWork.Repository<WalletHistory>().HardDeleteGuid(walletHistory.Id);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<WalletHistory, WalletHistoryResponse>(walletHistory);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<WalletHistoryResponse> Update(Guid id, WalletHistoryRequest request)
        {
            try
            {
                WalletHistory walletHistory = _unitOfWork.Repository<WalletHistory>()
                            .Find(x => x.Id == id);
                if (walletHistory == null)
                {
                    throw new Exception();
                }
                walletHistory = _mapper.Map(request, walletHistory);

                await _unitOfWork.Repository<WalletHistory>().UpdateDetached(walletHistory);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<WalletHistory, WalletHistoryResponse>(walletHistory);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
