using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace Service.LearnerAttendancesService
{
    public interface ILearnerAttendanceService
    {
        public Task<List<LearnerAttendanceResponse>> GetAll();

        public Task<LearnerAttendanceResponse> Get(Guid id);

        public Task<LearnerAttendanceResponse> Create(LearnerAttendanceRequest request);

        public Task<LearnerAttendanceResponse> Delete(Guid id);

        public Task<LearnerAttendanceResponse> Update(Guid id, LearnerAttendanceRequest request);
    }
}