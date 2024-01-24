using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Models;
using Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Service.AccountsService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace Service.CentersService
{
    public class CenterService : ICenterService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private IAccountService _accountService;
        public CenterService(IUnitOfWork unitOfWork, IMapper mapper, IAccountService accountService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _accountService = accountService;
        }

        public async Task<List<CenterResponse>> GetAll()
        {

            var list = await _unitOfWork.Repository<Center>().GetAll()
                                            .ProjectTo<CenterResponse>(_mapper.ConfigurationProvider)
                                            .ToListAsync();
            return list;
        }

        public async Task<CenterResponse> Create(CenterRequest request)
        {
            try
            {
                AccountResponse account = await _accountService.Create(new AccountRequest());
                var center = _mapper.Map<CenterRequest, Center>(request);
                center.Id = Guid.NewGuid();
                center.AccountId = account.Id;
                await _unitOfWork.Repository<Center>().InsertAsync(center);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<Center, CenterResponse>(center);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<CenterResponse> Delete(Guid id)
        {
            try
            {
                Center center = null;
                center = _unitOfWork.Repository<Center>()
                    .Find(p => p.Id == id);
                if (center == null)
                {
                    throw new Exception("Bi trung id");
                }
                await _unitOfWork.Repository<Center>().HardDeleteGuid(center.Id);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<Center, CenterResponse>(center);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CenterResponse> Update(Guid id, CenterRequest request)
        {
            try
            {
                Center center = _unitOfWork.Repository<Center>()
                            .Find(x => x.Id == id);
                if (center == null)
                {
                    throw new Exception();
                }
                center = _mapper.Map(request, center);

                await _unitOfWork.Repository<Center>().UpdateDetached(center);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<Center, CenterResponse>(center);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
