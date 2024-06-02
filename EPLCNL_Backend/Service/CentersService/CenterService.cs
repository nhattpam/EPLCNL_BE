using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Models;
using Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Service.AccountsService;
using Service.CoursesService;
using Service.TutorService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
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
        private ICourseService _courseService;
        private ITutorService _tutorService;
        public CenterService(IUnitOfWork unitOfWork, IMapper mapper,
            IAccountService accountService, ICourseService courseService, ITutorService tutorService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _accountService = accountService;
            _courseService = courseService;
            _tutorService = tutorService;
        }

        public async Task<List<CenterResponse>> GetAll()
        {

            var list = await _unitOfWork.Repository<Center>().GetAll()
                                            .ProjectTo<CenterResponse>(_mapper.ConfigurationProvider)
                                            .ToListAsync();
            return list;
        }


        public async Task<CenterResponse> Get(Guid id)
        {
            try
            {
                Center center = null;
                center = await _unitOfWork.Repository<Center>().GetAll()
                    .Include(a => a.Account)
                        .ThenInclude(a => a.Wallet)
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();

                if (center == null)
                {
                    throw new Exception("khong tim thay");
                }

                return _mapper.Map<Center, CenterResponse>(center);
            }
           
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<List<TutorResponse>> GetAllTutorsByCenter(Guid id)
        {
            var center = await _unitOfWork.Repository<Center>().GetAll()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (center == null)
            {
                // Handle the case where the center with the specified id is not found
                return null;
            }

            var tutors = _unitOfWork.Repository<Tutor>().GetAll()
                .Where(t => t.CenterId == id)
                .ProjectTo<TutorResponse>(_mapper.ConfigurationProvider)
                .ToList();

            return tutors;
        }

        public async Task<List<EnrollmentResponse>> GetAllEnrollmentsByCenter(Guid centerId)
        {
            var center = await _unitOfWork.Repository<Center>()
                .GetAll()
                .Where(x => x.Id == centerId)
                .FirstOrDefaultAsync();

            if (center == null)
            {
                // Handle the case where the center with the specified id is not found
                return null;
            }

            var enrollments = await _unitOfWork.Repository<Enrollment>()
                .GetAll()
                .Where(e => e.Transaction.Course.Tutor.CenterId == centerId)
                .ProjectTo<EnrollmentResponse>(_mapper.ConfigurationProvider)
                .ToListAsync();

            return enrollments;
        }


        public async Task<CenterResponse> Create(CenterRequest request)
        {
            // Set the UTC offset for UTC+7
            TimeSpan utcOffset = TimeSpan.FromHours(7);

            // Get the current UTC time
            DateTime utcNow = DateTime.UtcNow;

            // Convert the UTC time to UTC+7
            DateTime localTime = utcNow + utcOffset;
            try
            {
                AccountResponse account = await _accountService.Create(new AccountRequest()
                {
                    Email = request.Email,
                    Password = request.Email,
                    FullName = request.Name,
                    RoleId = new Guid("14191B0A-2EC2-48E3-9EDE-C34D5DE0BA32"),
                    IsActive = false,
                    PhoneNumber = request.PhoneNumber,
                });
                var center = _mapper.Map<CenterRequest, Center>(request);
                center.Id = Guid.NewGuid();
                center.AccountId = account.Id;
                center.IsActive = false;
                center.CreatedDate = localTime;
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
                    throw new Exception("khong tim thay");
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
            // Set the UTC offset for UTC+7
            TimeSpan utcOffset = TimeSpan.FromHours(7);

            // Get the current UTC time
            DateTime utcNow = DateTime.UtcNow;

            // Convert the UTC time to UTC+7
            DateTime localTime = utcNow + utcOffset;
            try
            {
                Center center = _unitOfWork.Repository<Center>()
                            .Find(x => x.Id == id);
                if (center == null)
                {
                    throw new Exception();
                }
                center = _mapper.Map(request, center);
                center.UpdatedDate = localTime;

                AccountResponse account = await _accountService.Get(center.AccountId);

                AccountRequest accountRequest = new AccountRequest();
                if(center.IsActive == true)
                {
                    accountRequest.IsActive = true;
                    accountRequest.UpdatedDate = localTime;
                    accountRequest.Email = account.Email;
                    accountRequest.Password = account.Password;
                    accountRequest.FullName = account.FullName;
                    accountRequest.PhoneNumber = account.PhoneNumber;
                    accountRequest.ImageUrl = account.ImageUrl;
                    accountRequest.DateOfBirth = account.DateOfBirth;
                    accountRequest.Gender = account.Gender;
                    accountRequest.Address = account.Address;
                    accountRequest.RoleId = account.RoleId;
                    accountRequest.CreatedDate = account.CreatedDate;
                }
                else
                {
                    accountRequest.IsActive = false;
                    accountRequest.UpdatedDate = localTime;
                    accountRequest.Email = account.Email;
                    accountRequest.Password = account.Password;
                    accountRequest.FullName = account.FullName;
                    accountRequest.PhoneNumber = account.PhoneNumber;
                    accountRequest.ImageUrl = account.ImageUrl;
                    accountRequest.DateOfBirth = account.DateOfBirth;
                    accountRequest.Gender = account.Gender;
                    accountRequest.Address = account.Address;
                    accountRequest.RoleId = account.RoleId;
                    accountRequest.CreatedDate = account.CreatedDate;
                }

                await _accountService.Update(account.Id, accountRequest);   

                await _unitOfWork.Repository<Center>().UpdateDetached(center);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<Center, CenterResponse>(center);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public async Task<List<CourseResponse>> GetAllCoursesByCenter(Guid id)
        {
            // Retrieve the center
            var center = await _unitOfWork.Repository<Center>().GetAll()
                .Where(x => x.Id == id)
                .FirstOrDefaultAsync();

            if (center == null)
            {
                // Handle the case where the course with the specified id is not found
                return null;
            }

            // Retrieve courses for the course
            var courses = await _unitOfWork.Repository<Course>().GetAll()
                .Where(t => t.Tutor.CenterId == id)
                .ProjectTo<CourseResponse>(_mapper.ConfigurationProvider)
                                            .ToListAsync();

            return courses;
        }
    }
}
