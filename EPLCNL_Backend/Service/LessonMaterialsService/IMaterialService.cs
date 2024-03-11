using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace Service.MaterialsService
{
    public interface IMaterialService
    {
        public Task<List<MaterialResponse>> GetAll();

        public Task<MaterialResponse> Get(Guid id);

        public Task<MaterialResponse> Create(MaterialRequest request);

        public Task<MaterialResponse> Delete(Guid id);

        public Task<MaterialResponse> Update(Guid id, MaterialRequest request);
    }
}