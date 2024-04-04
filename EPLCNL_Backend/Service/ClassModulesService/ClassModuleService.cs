using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Models;
using Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Service.AssignmentAttemptsService;
using Service.AttendancesService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace Service.ClassModulesService
{
    public class ClassModuleService : IClassModuleService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private readonly IAttendanceService _attendanceService;
        public ClassModuleService(IUnitOfWork unitOfWork, IMapper mapper, IAttendanceService attendanceService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _attendanceService = attendanceService;
        }

        public async Task<List<ClassModuleResponse>> GetAll()
        {

            var list = await _unitOfWork.Repository<ClassModule>().GetAll()
                                            .ProjectTo<ClassModuleResponse>(_mapper.ConfigurationProvider)
                                            .ToListAsync();
            return list;
        }

        public async Task<ClassModuleResponse> Get(Guid id)
        {
            try
            {
                ClassModule classModule = null;
                classModule = await _unitOfWork.Repository<ClassModule>().GetAll()
                        .Include(a => a.Course)
                        .Include(a => a.ClassLesson)
                        .Include(a => a.Attendance)
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();

                if (classModule == null)
                {
                    throw new Exception("khong tim thay");
                }

                return _mapper.Map<ClassModule, ClassModuleResponse>(classModule);
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }


        public async Task<ClassModuleResponse> Create(ClassModuleRequest request)
        {
            // Set the UTC offset for UTC+7
            TimeSpan utcOffset = TimeSpan.FromHours(7);

            // Get the current UTC time
            DateTime utcNow = DateTime.UtcNow;

            // Convert the UTC time to UTC+7
            DateTime localTime = utcNow + utcOffset;
            try
            {
                var classModule = _mapper.Map<ClassModuleRequest, ClassModule>(request);
                classModule.Id = Guid.NewGuid();
                classModule.CreatedDate = localTime;
                await _unitOfWork.Repository<ClassModule>().InsertAsync(classModule);
                await _unitOfWork.CommitAsync();

                //create attendance
                var attendance = new AttendanceRequest()
                {
                    ClassModuleId = classModule.Id,
                };
                await _attendanceService.Create(attendance);

                return _mapper.Map<ClassModule, ClassModuleResponse>(classModule);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<ClassModuleResponse> Delete(Guid id)
        {
            try
            {
                ClassModule classModule = null;
                classModule = _unitOfWork.Repository<ClassModule>()
                    .Find(p => p.Id == id);
                if (classModule == null)
                {
                    throw new Exception("Bi trung id");
                }
                await _unitOfWork.Repository<ClassModule>().HardDeleteGuid(classModule.Id);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<ClassModule, ClassModuleResponse>(classModule);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<ClassModuleResponse> Update(Guid id, ClassModuleRequest request)
        {
            // Set the UTC offset for UTC+7
            TimeSpan utcOffset = TimeSpan.FromHours(7);

            // Get the current UTC time
            DateTime utcNow = DateTime.UtcNow;

            // Convert the UTC time to UTC+7
            DateTime localTime = utcNow + utcOffset;
            try
            {
                ClassModule classModule = _unitOfWork.Repository<ClassModule>()
                            .Find(x => x.Id == id);
                if (classModule == null)
                {
                    throw new Exception();
                }
                classModule.UpdatedDate = localTime;
                classModule = _mapper.Map(request, classModule);

                await _unitOfWork.Repository<ClassModule>().UpdateDetached(classModule);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<ClassModule, ClassModuleResponse>(classModule);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
