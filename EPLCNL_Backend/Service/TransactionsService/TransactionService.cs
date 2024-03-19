using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Models;
using Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Service.EnrollmentsService;
using Service.LearnersService;
using Service.WalletHistoriesService;
using Service.WalletsService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace Service.TransactionsService
{
    public class TransactionService: ITransactionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private ILearnerService _learnerService;
        private IEnrollmentService _enrollmentService;
        private IWalletService _walletService;
        private IWalletHistoryService _walletHistoryService;
        public TransactionService(IUnitOfWork unitOfWork, IMapper mapper,
            ILearnerService learnerService, IEnrollmentService enrollmentService
            , IWalletService walletService, IWalletHistoryService walletHistoryService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _learnerService = learnerService;
            _enrollmentService = enrollmentService;
            _walletService = walletService;
            _walletHistoryService = walletHistoryService;
        }

        public async Task<List<TransactionResponse>> GetAll()
        {

            var list = await _unitOfWork.Repository<Transaction>().GetAll()
                                            .ProjectTo<TransactionResponse>(_mapper.ConfigurationProvider)
                                            .ToListAsync();
            return list;
        }

        public async Task<TransactionResponse> Get(Guid id)
        {
            try
            {
                Transaction transaction = null;
                transaction = await _unitOfWork.Repository<Transaction>().GetAll()
                     .AsNoTracking()
                     .Include(x => x.PaymentMethod)
                     .Include(x => x.Learner)
                     .ThenInclude(x => x.Account)
                     .Include(x => x.Course)
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();

                if (transaction == null)
                {
                    throw new Exception("khong tim thay");
                }

                return _mapper.Map<Transaction, TransactionResponse>(transaction);
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
       
        public async Task<TransactionResponse> Create(TransactionRequest request)
        {
            // Set the UTC offset for UTC+7
            TimeSpan utcOffset = TimeSpan.FromHours(7);

            // Get the current UTC time
            DateTime utcNow = DateTime.UtcNow;

            // Convert the UTC time to UTC+7
            DateTime localTime = utcNow + utcOffset;
            try
            {
                var transaction = _mapper.Map<TransactionRequest, Transaction>(request);
                transaction.Id = Guid.NewGuid();
                transaction.TransactionDate = localTime;
                transaction.Status = "PROCESSING";
                await _unitOfWork.Repository<Transaction>().InsertAsync(transaction);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<Transaction, TransactionResponse>(transaction);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<TransactionResponse> Delete(Guid id)
        {
            try
            {
                Transaction transaction = null;
                transaction = _unitOfWork.Repository<Transaction>()
                    .Find(p => p.Id == id);
                if (transaction == null)
                {
                    throw new Exception("Bi trung id");
                }
                await _unitOfWork.Repository<Transaction>().HardDeleteGuid(transaction.Id);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<Transaction, TransactionResponse>(transaction);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<TransactionResponse> Update(Guid id, TransactionRequest request)
        {
            try
            {
                Transaction transaction = _unitOfWork.Repository<Transaction>()
                            .Find(x => x.Id == id);
                if (transaction == null)
                {
                    throw new Exception();
                }
                transaction = _mapper.Map(request, transaction);

                await _unitOfWork.Repository<Transaction>().UpdateDetached(transaction);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<Transaction, TransactionResponse>(transaction);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<bool> PayByWallet(Guid id)
        {
            var transaction = await Get(id);
            if (transaction == null)
            {
                throw new Exception();
            }
            else
            {
                try
                {
                    var learner = await _learnerService.Get(transaction.LearnerId);
                    var enrollmentModel = new EnrollmentRequest
                    {
                        TransactionId = transaction.Id,
                        EnrolledDate = transaction.TransactionDate,
                        Status = "ONGOING",
                        TotalGrade = 0
                    };

                    await _enrollmentService.Create(enrollmentModel);

                    var adminWallet = await _walletService.Get(Guid.Parse("188e9df9-be4b-4531-858e-098ff8c3735c"));
                    adminWallet.Balance = adminWallet.Balance + (transaction.Amount /24000);
                    // Now, you need to create a request object to update the wallet
                    var updateAdminWalletRequest = new WalletRequest
                    {
                        AccountId = new Guid("9b868733-8ab1-4191-92ab-65d1b82863c3"),
                        Balance = adminWallet.Balance
                    };

                    // Call the update method with the request object
                    await _walletService.Update(adminWallet.Id, updateAdminWalletRequest);

                    var adminWalletHistory = new WalletHistoryRequest
                    {
                        WalletId = adminWallet.Id,
                        Note = $"+ {transaction.Amount / 24000}$ from {learner?.Account?.FullName} by transaction {transaction.Id} at {transaction.TransactionDate}",
                        TransactionDate = transaction.TransactionDate,
                    };

                    await _walletHistoryService.Create(adminWalletHistory);

                    var learnerWallet = await _walletService.Get(learner.Account?.Wallet?.Id ?? new Guid());
                    learnerWallet.Balance = learnerWallet.Balance - (transaction.Amount / 24000);
                    var updateLearnerWalletRequest = new WalletRequest
                    {
                        AccountId = learner.AccountId,
                        Balance = learnerWallet.Balance
                    };

                    await _walletService.Update(learnerWallet.Id, updateLearnerWalletRequest);

                    var learnerWalletHistory = new WalletHistoryRequest
                    {
                        WalletId = learnerWallet.Id,
                        Note = $"- {transaction.Amount /24000}$ by transaction for course {transaction.Course?.Name} at {transaction.TransactionDate}",
                        TransactionDate = transaction.TransactionDate,
                    };

                    await _walletHistoryService.Create(learnerWalletHistory);
                    return true;
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }

        }
    }
}
