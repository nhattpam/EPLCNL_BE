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

        public async Task<StaffResponse> Get(Guid id)
        {
            try
            {
                Staff staff = null;
                staff = await _unitOfWork.Repository<Staff>().GetAll()
                     .AsNoTracking()
                     .Include(x => x.Account)
                        .ThenInclude(x => x.Wallet)
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();

                if (staff == null)
                {
                    throw new Exception("khong tim thay");
                }

                return _mapper.Map<Staff, StaffResponse>(staff);
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<TutorResponse>> GetAllTutorsByStaff(Guid id)
        {
            var staff = await _unitOfWork.Repository<Staff>().GetAll()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (staff == null)
            {
                // Handle the case where the center with the specified id is not found
                return null;
            }

            var tutors = _unitOfWork.Repository<Tutor>().GetAll()
                .Where(t => t.StaffId == id)
                .ProjectTo<TutorResponse>(_mapper.ConfigurationProvider)
                .ToList();

            return tutors;
        }

        public async Task<List<CenterResponse>> GetAllCentersByStaff(Guid id)
        {
            var staff = await _unitOfWork.Repository<Staff>().GetAll()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (staff == null)
            {
                // Handle the case where the center with the specified id is not found
                return null;
            }

            var centers = _unitOfWork.Repository<Center>().GetAll()
                .Where(t => t.StaffId == id)
                .ProjectTo<CenterResponse>(_mapper.ConfigurationProvider)
                .ToList();

            return centers;
        }

        public async Task<List<ReportResponse>> GetAllReportsByStaff(Guid id)
        {
            try
            {
                // Retrieve tutors associated with the staff
                var tutors = await GetAllTutorsByStaff(id);

                // Extract tutor Ids
                var tutorIds = tutors.Select(t => t.Id).ToList();

                // Retrieve reports associated with the tutors
                var tutorReports = await _unitOfWork.Repository<Report>()
                    .GetAll()
                    .Where(r => tutorIds.Contains(r.Course.TutorId.Value))
                    .ProjectTo<ReportResponse>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                // Retrieve centers associated with the staff
                var centers = await GetAllCentersByStaff(id);

                // Extract center Ids
                var centerIds = centers.Select(c => c.Id).ToList();

                // Retrieve reports associated with the centers
                var centerReports = await _unitOfWork.Repository<Report>()
                    .GetAll()
                    .Where(r => centerIds.Contains(r.Course.Tutor.CenterId.Value))
                    .ProjectTo<ReportResponse>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                // Merge reports from both cases
                var allReports = tutorReports.Concat(centerReports).ToList();

                return allReports;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving reports for the staff", ex);
            }
        }


        public async Task<List<CourseResponse>> GetAllCoursesByStaff(Guid id)
        {
            try
            {
                // Retrieve tutors associated with the staff
                var tutors = await GetAllTutorsByStaff(id);

                // Extract tutor Ids
                var tutorIds = tutors.Select(t => t.Id).ToList();

                // Retrieve courses associated with the tutors
                var tutorCourses = await _unitOfWork.Repository<Course>()
                    .GetAll()
                    .Where(c => tutorIds.Contains(c.TutorId.Value))
                    .ProjectTo<CourseResponse>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                // Retrieve centers associated with the staff
                var centers = await GetAllCentersByStaff(id);

                // Extract center Ids
                var centerIds = centers.Select(c => c.Id).ToList();

                // Retrieve courses associated with the centers
                var centerCourses = await _unitOfWork.Repository<Course>()
                    .GetAll()
                    .Where(c => centerIds.Contains(c.Tutor.CenterId.Value))
                    .ProjectTo<CourseResponse>(_mapper.ConfigurationProvider)
                    .ToListAsync();

                // Merge courses from both cases
                var allCourses = tutorCourses.Concat(centerCourses).ToList();

                return allCourses;
            }
            catch (Exception ex)
            {
                throw new Exception("Error retrieving courses for the staff", ex);
            }
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
