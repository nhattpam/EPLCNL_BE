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

        public async Task<List<AccountSurveyResponse>> GetAccountSurveys()
        {

            var list = await _unitOfWork.Repository<AccountSurvey>().GetAll()
                                            .ProjectTo<AccountSurveyResponse>(_mapper.ConfigurationProvider)
                                            .ToListAsync();
            return list;
        }

        public async Task<AccountSurveyResponse> Create(AccountSurveyRequest request)
        {
            try
            {
                var accountsurvey = _mapper.Map<AccountSurveyRequest, AccountSurvey>(request);
                accountsurvey.Id = Guid.NewGuid();
                await _unitOfWork.Repository<AccountSurvey>().InsertAsync(accountsurvey);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<AccountSurvey, AccountSurveyResponse>(accountsurvey);
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
                AccountSurvey accountsurvey = null;
                accountsurvey = _unitOfWork.Repository<AccountSurvey>()
                    .Find(p => p.Id == id);
                if (accountsurvey == null)
                {
                    throw new Exception("Bi trung id");
                }
                await _unitOfWork.Repository<AccountSurvey>().HardDeleteGuid(accountsurvey.Id);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<AccountSurvey, AccountSurveyResponse>(accountsurvey);
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
                AccountSurvey accountsurvey = _unitOfWork.Repository<AccountSurvey>()
                            .Find(x => x.Id == id);
                if (accountsurvey == null)
                {
                    throw new Exception();
                }
                accountsurvey = _mapper.Map(request, accountsurvey);

                await _unitOfWork.Repository<AccountSurvey>().UpdateDetached(accountsurvey);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<AccountSurvey, AccountSurveyResponse>(accountsurvey);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
