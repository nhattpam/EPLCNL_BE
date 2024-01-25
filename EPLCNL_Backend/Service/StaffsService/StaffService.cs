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

namespace Service.StaffsService
{
    public class StaffService : IStaffService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private IAccountService _accountService;
        public StaffService(IUnitOfWork unitOfWork, IMapper mapper, IAccountService accountService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _accountService = accountService;
        }

        public async Task<List<StaffResponse>> GetAll()
        {
            var list = await _unitOfWork.Repository<Staff>()
                .GetAll()
                .ProjectTo<StaffResponse>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return list;
        }


        public async Task<StaffResponse> Create(StaffRequest request)
        {
            try
            {
                var Staff = _mapper.Map<StaffRequest, Staff>(request);
                Staff.Id = Guid.NewGuid();
                await _unitOfWork.Repository<Staff>().InsertAsync(Staff);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<Staff, StaffResponse>(Staff);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<StaffResponse> Delete(Guid id)
        {
            try
            {
                Staff Staff = null;
                Staff = _unitOfWork.Repository<Staff>()
                    .Find(p => p.Id == id);
                if (Staff == null)
                {
                    throw new Exception("Bi trung id");
                }
                await _unitOfWork.Repository<Staff>().HardDeleteGuid(Staff.Id);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<Staff, StaffResponse>(Staff);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<StaffResponse> Update(Guid id, StaffRequest request)
        {
            try
            {
                Staff Staff = _unitOfWork.Repository<Staff>()
                            .Find(x => x.Id == id);
                if (Staff == null)
                {
                    throw new Exception();
                }
                Staff = _mapper.Map(request, Staff);

                await _unitOfWork.Repository<Staff>().UpdateDetached(Staff);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<Staff, StaffResponse>(Staff);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
