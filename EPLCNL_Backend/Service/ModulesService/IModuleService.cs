using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace Service.ModulesService
{
    public interface IModuleService
    {
        public Task<List<ModuleResponse>> GetModules();

        public Task<ModuleResponse> Create(ModuleRequest request);

        public Task<ModuleResponse> Delete(Guid id);

        public Task<ModuleResponse> Update(Guid id, ModuleRequest request);
    }
}
