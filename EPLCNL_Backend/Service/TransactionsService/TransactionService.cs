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

namespace Service.TransactionsService
{
    public class TransactionService: ITransactionService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public TransactionService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<TransactionResponse>> GetAll()
        {

            var list = await _unitOfWork.Repository<Transaction>().GetAll()
                                            .ProjectTo<TransactionResponse>(_mapper.ConfigurationProvider)
                                            .ToListAsync();
            return list;
        }

        public async Task<TransactionResponse> Create(TransactionRequest request)
        {
            try
            {
                var transaction = _mapper.Map<TransactionRequest, Transaction>(request);
                transaction.Id = Guid.NewGuid();
                transaction.TransactionDate = DateTime.Now;
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
    }
}
