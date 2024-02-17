using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace Service.ReportsService
{
    public interface IReportService
    {
        public Task<List<ReportResponse>> GetAll();

        public Task<ReportResponse> Get(Guid id);

        public Task<ReportResponse> Create(ReportRequest request);

        public Task<ReportResponse> Delete(Guid id);

        public Task<ReportResponse> Update(Guid id, ReportRequest request);
    }
}