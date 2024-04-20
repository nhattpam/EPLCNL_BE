using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace Service.SalariesService
{
    public interface ISalaryService
    {
        Task<List<SalaryResponse>> GetAll();
        Task<SalaryResponse> Get(Guid? id);
        Task<SalaryResponse> Create(SalaryRequest request);
        Task<SalaryResponse> Update(Guid id, SalaryRequest request);
        Task<SalaryResponse> Delete(Guid id);
    }
}