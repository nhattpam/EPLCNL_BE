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

namespace Service.AccountsService
{
    public class AccountService : IAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public AccountService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<AccountResponse>> GetAll()
        {

            var list = await _unitOfWork.Repository<Account>().GetAll()
                                            .ProjectTo<AccountResponse>(_mapper.ConfigurationProvider)
                                            .ToListAsync();
            return list;
        }

        public async Task<AccountResponse> Create(AccountRequest request)
        {
            try
            {
                var account = _mapper.Map<AccountRequest, Account>(request);
                account.Id = Guid.NewGuid();
                account.CreatedDate = DateTime.Now;
                account.IsDeleted = false;
                account.IsActive = false;
                await _unitOfWork.Repository<Account>().InsertAsync(account);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<Account, AccountResponse>(account);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<AccountResponse> Delete(Guid id)
        {
            try
            {
                Account account = null;
                account = _unitOfWork.Repository<Account>()
                    .Find(p => p.Id == id);
                if (account == null)
                {
                    throw new Exception("Id is not existed");
                }
                await _unitOfWork.Repository<Account>().HardDeleteGuid(account.Id);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<Account, AccountResponse>(account);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<AccountResponse> Update(Guid id, AccountRequest request)
        {
            try
            {
                Account account = _unitOfWork.Repository<Account>()
                            .Find(x => x.Id == id);
                if (account == null)
                {
                    throw new Exception();
                }
                account = _mapper.Map(request, account);
                account.UpdatedDate = DateTime.Now;

                await _unitOfWork.Repository<Account>().UpdateDetached(account);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<Account, AccountResponse>(account);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<AccountResponse> Login(LoginMem accc)
        {
            { 
                Account account = _unitOfWork.Repository<Account>()
                           .Find(x => x.Email.Equals(accc.Email) && x.Password.Equals(accc.Password));
                return _mapper.Map<Account, AccountResponse>(account);
            }

        }
    }
}
