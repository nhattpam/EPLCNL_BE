using Data.Models;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace Service
{
    public interface IRoleService
    {
        public  Task<List<RoleResponse>> GetAll();

        public Task<RoleResponse> Get(Guid id);

        public Task<RoleResponse> Create(RoleRequest request);

        public Task<RoleResponse> Delete(Guid id);

        public Task<RoleResponse> Update(Guid id, RoleRequest request);
    }
}