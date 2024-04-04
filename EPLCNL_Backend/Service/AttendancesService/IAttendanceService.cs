using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace Service.AttendancesService
{
    public interface IAttendanceService
    {
        public Task<List<AttendanceResponse>> GetAll();

        public Task<AttendanceResponse> Get(Guid? id);

        public Task<List<LearnerAttendanceResponse>> GetLearnerAttendanceByAttendance(Guid id);

        public Task<AttendanceResponse> Create(AttendanceRequest request);

        public Task<AttendanceResponse> Delete(Guid id);

        public Task<AttendanceResponse> Update(Guid id, AttendanceRequest request);
    }
}