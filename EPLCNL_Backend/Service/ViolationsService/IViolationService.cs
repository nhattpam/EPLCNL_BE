using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace Service.ViolationsService
{
    public interface IViolationService
    {
        public Task<List<ViolationResponse>> GetAll();

        public Task<ViolationResponse> Get(Guid id);

        public Task<ViolationResponse> Create(ViolationRequest request);

        public Task<ViolationResponse> Delete(Guid id);

        public Task<ViolationResponse> Update(Guid id, ViolationRequest request);
    }
}