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

namespace Service.AttendancesService
{
    public class AttendanceService: IAttendanceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public AttendanceService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<AttendanceResponse>> GetAll()
        {

            var list = await _unitOfWork.Repository<Attendance>().GetAll()
                                            .ProjectTo<AttendanceResponse>(_mapper.ConfigurationProvider)
                                            .ToListAsync();
            return list;
        }

        public async Task<AttendanceResponse> Get(Guid? id)
        {
            try
            {
                Attendance attendance = null;
                attendance = await _unitOfWork.Repository<Attendance>().GetAll()
                     .AsNoTracking()
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();

                if (attendance == null)
                {
                    throw new Exception("khong tim thay");
                }

                return _mapper.Map<Attendance, AttendanceResponse>(attendance);
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<AttendanceResponse> Create(AttendanceRequest request)
        {
            // Set the UTC offset for UTC+7
            TimeSpan utcOffset = TimeSpan.FromHours(7);

            // Get the current UTC time
            DateTime utcNow = DateTime.UtcNow;

            // Convert the UTC time to UTC+7
            DateTime localTime = utcNow + utcOffset;
            try
            {
                var attendance = _mapper.Map<AttendanceRequest, Attendance>(request);
                attendance.Id = Guid.NewGuid();
                attendance.CreatedDate = localTime;
                await _unitOfWork.Repository<Attendance>().InsertAsync(attendance);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<Attendance, AttendanceResponse>(attendance);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<AttendanceResponse> Delete(Guid id)
        {
            try
            {
                Attendance attendance = null;
                attendance = _unitOfWork.Repository<Attendance>()
                    .Find(p => p.Id == id);
                if (attendance == null)
                {
                    throw new Exception("Bi trung id");
                }
                await _unitOfWork.Repository<Attendance>().HardDeleteGuid(attendance.Id);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<Attendance, AttendanceResponse>(attendance);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<AttendanceResponse> Update(Guid id, AttendanceRequest request)
        {
            // Set the UTC offset for UTC+7
            TimeSpan utcOffset = TimeSpan.FromHours(7);

            // Get the current UTC time
            DateTime utcNow = DateTime.UtcNow;

            // Convert the UTC time to UTC+7
            DateTime localTime = utcNow + utcOffset;
            try
            {
                Attendance attendance = _unitOfWork.Repository<Attendance>()
                            .Find(x => x.Id == id);
                if (attendance == null)
                {
                    throw new Exception();
                }
                attendance = _mapper.Map(request, attendance);
                attendance.UpdatedDate = localTime;
                await _unitOfWork.Repository<Attendance>().UpdateDetached(attendance);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<Attendance, AttendanceResponse>(attendance);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<LearnerAttendanceResponse>> GetLearnerAttendanceByAttendance(Guid id)
        {
            try
            {
                var learnerAttendances = await _unitOfWork.Repository<LearnerAttendance>().GetAll()
                    .Include(x => x.Attendance)
                    .Where(x => x.AttendanceId == id)
                    .ProjectTo<LearnerAttendanceResponse>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                if (learnerAttendances == null)
                {
                    throw new Exception("khong tim thay");
                }

                return learnerAttendances;
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
