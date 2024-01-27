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

namespace Service.AccountSurveysService
{
    public class AccountSurveyService: IAccountSurveyService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public AccountSurveyService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<AccountSurveyResponse>> GetAll()
        {

            var list = await _unitOfWork.Repository<AccountSurvey>().GetAll()
                                            .ProjectTo<AccountSurveyResponse>(_mapper.ConfigurationProvider)
                                            .ToListAsync();
            return list;
        }
        public async Task<AccountSurveyResponse> Get(Guid id)
        {
            try
            {
                AccountSurvey accountSurvey = null;
                accountSurvey = await _unitOfWork.Repository<AccountSurvey>().GetAll()
                     .AsNoTracking()
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();

                if (accountSurvey == null)
                {
                    throw new Exception("khong tim thay");
                }

                return _mapper.Map<AccountSurvey, AccountSurveyResponse>(accountSurvey);
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<AccountSurveyResponse> Create(AccountSurveyRequest request)
        {
            try
            {
                var accountSurvey = _mapper.Map<AccountSurveyRequest, AccountSurvey>(request);
                accountSurvey.Id = Guid.NewGuid();
                await _unitOfWork.Repository<AccountSurvey>().InsertAsync(accountSurvey);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<AccountSurvey, AccountSurveyResponse>(accountSurvey);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<AccountSurveyResponse> Delete(Guid id)
        {
            try
            {
                AccountSurvey accountSurvey = null;
                accountSurvey = _unitOfWork.Repository<AccountSurvey>()
                    .Find(p => p.Id == id);
                if (accountSurvey == null)
                {
                    throw new Exception("Bi trung id");
                }
                await _unitOfWork.Repository<AccountSurvey>().HardDeleteGuid(accountSurvey.Id);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<AccountSurvey, AccountSurveyResponse>(accountSurvey);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<AccountSurveyResponse> Update(Guid id, AccountSurveyRequest request)
        {
            try
            {
                AccountSurvey accountSurvey = _unitOfWork.Repository<AccountSurvey>()
                            .Find(x => x.Id == id);
                if (accountSurvey == null)
                {
                    throw new Exception();
                }
                accountSurvey = _mapper.Map(request, accountSurvey);

                await _unitOfWork.Repository<AccountSurvey>().UpdateDetached(accountSurvey);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<AccountSurvey, AccountSurveyResponse>(accountSurvey);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
