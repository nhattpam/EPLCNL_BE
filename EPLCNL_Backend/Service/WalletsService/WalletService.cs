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

namespace Service.WalletsService
{
    public class WalletService: IWalletService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public WalletService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<WalletResponse>> GetAll()
        {

            var list = await _unitOfWork.Repository<Wallet>().GetAll()
                                            .ProjectTo<WalletResponse>(_mapper.ConfigurationProvider)
                                            .ToListAsync();
            return list;
        }
        public async Task<WalletResponse> Get(Guid id)
        {
            try
            {
                Wallet wallet = null;
                wallet = await _unitOfWork.Repository<Wallet>().GetAll()
                     .AsNoTracking()
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();

                if (wallet == null)
                {
                    throw new Exception("khong tim thay");
                }

                return _mapper.Map<Wallet, WalletResponse>(wallet);
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
        

        public async Task<WalletResponse> Create(WalletRequest request)
        {
            try
            {
                var wallet = _mapper.Map<WalletRequest, Wallet>(request);
                wallet.Id = Guid.NewGuid();
                await _unitOfWork.Repository<Wallet>().InsertAsync(wallet);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<Wallet, WalletResponse>(wallet);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<WalletResponse> Delete(Guid id)
        {
            try
            {
                Wallet wallet = null;
                wallet = _unitOfWork.Repository<Wallet>()
                    .Find(p => p.Id == id);
                if (wallet == null)
                {
                    throw new Exception("Bi trung id");
                }
                await _unitOfWork.Repository<Wallet>().HardDeleteGuid(wallet.Id);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<Wallet, WalletResponse>(wallet);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<WalletResponse> Update(Guid id, WalletRequest request)
        {
            try
            {
                Wallet wallet = _unitOfWork.Repository<Wallet>()
                            .Find(x => x.Id == id);
                if (wallet == null)
                {
                    throw new Exception();
                }
                wallet = _mapper.Map(request, wallet);

                await _unitOfWork.Repository<Wallet>().UpdateDetached(wallet);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<Wallet, WalletResponse>(wallet);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<WalletHistoryResponse>> GetWalletHistoryByWallet(Guid id)
        {
            try
            {
                var walletHistories = await _unitOfWork.Repository<WalletHistory>().GetAll()
                    .Include(x => x.Wallet)
                    .Where(x => x.WalletId == id)
                    .ProjectTo<WalletHistoryResponse>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                if (walletHistories == null)
                {
                    throw new Exception("khong tim thay");
                }

                return walletHistories;
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
