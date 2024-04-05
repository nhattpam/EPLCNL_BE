using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Models;
using Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Service.CoursesService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace Service.LearnerAttendancesService
{
    public class LearnerAttendanceService: ILearnerAttendanceService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private readonly ICourseService _courseService;
        public LearnerAttendanceService(IUnitOfWork unitOfWork, IMapper mapper, ICourseService courseService)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _courseService = courseService;
        }

        public async Task<List<LearnerAttendanceResponse>> GetAll()
        {

            var list = await _unitOfWork.Repository<LearnerAttendance>().GetAll()
                                            .ProjectTo<LearnerAttendanceResponse>(_mapper.ConfigurationProvider)
                                            .ToListAsync();
            return list;
        }

        public async Task<LearnerAttendanceResponse> Get(Guid id)
        {
            try
            {
                LearnerAttendance learnerAttendance = null;
                learnerAttendance = await _unitOfWork.Repository<LearnerAttendance>().GetAll()
                     .AsNoTracking()
                     .Include(x => x.Attendance)
                     .Include(x => x.Learner)
                     .ThenInclude(x => x.Account)
                    .Where(x => x.Id == id)
                    .FirstOrDefaultAsync();

                if (learnerAttendance == null)
                {
                    throw new Exception("khong tim thay");
                }

                return _mapper.Map<LearnerAttendance, LearnerAttendanceResponse>(learnerAttendance);
            }

            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<LearnerAttendanceResponse> Create(LearnerAttendanceRequest request)
        {
            // Set the UTC offset for UTC+7
            TimeSpan utcOffset = TimeSpan.FromHours(7);

            // Get the current UTC time
            DateTime utcNow = DateTime.UtcNow;

            // Convert the UTC time to UTC+7
            DateTime localTime = utcNow + utcOffset;

            try
            {
                var learnerAttendance = _mapper.Map<LearnerAttendanceRequest, LearnerAttendance>(request);
                learnerAttendance.Id = Guid.NewGuid();
                learnerAttendance.CreatedDate = localTime;

                // Insert the learnerAttendance
                await _unitOfWork.Repository<LearnerAttendance>().InsertAsync(learnerAttendance);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<LearnerAttendance, LearnerAttendanceResponse>(learnerAttendance);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }



        public async Task<LearnerAttendanceResponse> Delete(Guid id)
        {
            try
            {
                LearnerAttendance learnerAttendance = null;
                learnerAttendance = _unitOfWork.Repository<LearnerAttendance>()
                    .Find(p => p.Id == id);
                if (learnerAttendance == null)
                {
                    throw new Exception("Bi trung id");
                }
                await _unitOfWork.Repository<LearnerAttendance>().HardDeleteGuid(learnerAttendance.Id);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<LearnerAttendance, LearnerAttendanceResponse>(learnerAttendance);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<LearnerAttendanceResponse> Update(Guid id, LearnerAttendanceRequest request)
        {
            // Set the UTC offset for UTC+7
            TimeSpan utcOffset = TimeSpan.FromHours(7);

            // Get the current UTC time
            DateTime utcNow = DateTime.UtcNow;

            // Convert the UTC time to UTC+7
            DateTime localTime = utcNow + utcOffset;
            try
            {
                LearnerAttendance learnerAttendance = _unitOfWork.Repository<LearnerAttendance>()
                            .Find(x => x.Id == id);
                if (learnerAttendance == null)
                {
                    throw new Exception();
                }
                learnerAttendance = _mapper.Map(request, learnerAttendance);
                learnerAttendance.UpdatedDate = localTime;
                await _unitOfWork.Repository<LearnerAttendance>().UpdateDetached(learnerAttendance);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<LearnerAttendance, LearnerAttendanceResponse>(learnerAttendance);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
