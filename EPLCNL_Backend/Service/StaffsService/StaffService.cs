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

namespace Service.StaffsService
{
    public class StaffService : IStaffService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public StaffService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<StaffResponse>> GetAll()
        {

            var list = await _unitOfWork.Repository<Staff>().GetAll()
                                            .ProjectTo<StaffResponse>(_mapper.ConfigurationProvider)
                                            .ToListAsync();
            return list;
        }

        public async Task<StaffResponse> Create(StaffRequest request)
        {
            try
            {
                var staff = _mapper.Map<StaffRequest, Staff>(request);
                staff.Id = Guid.NewGuid();
                await _unitOfWork.Repository<Staff>().InsertAsync(staff);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<Staff, StaffResponse>(staff);
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
                Staff staff = null;
                staff = _unitOfWork.Repository<Staff>()
                    .Find(p => p.Id == id);
                if (staff == null)
                {
                    throw new Exception("Bi trung id");
                }
                await _unitOfWork.Repository<Staff>().HardDeleteGuid(staff.Id);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<Staff, StaffResponse>(staff);
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
                Staff staff = _unitOfWork.Repository<Staff>()
                            .Find(x => x.Id == id);
                if (staff == null)
                {
                    throw new Exception();
                }
                staff = _mapper.Map(request, staff);

                await _unitOfWork.Repository<Staff>().UpdateDetached(staff);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<Staff, StaffResponse>(staff);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
