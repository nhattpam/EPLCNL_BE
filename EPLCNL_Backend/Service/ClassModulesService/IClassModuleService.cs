using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace Service.ClassModulesService
{
    public interface IClassModuleService
    {
        public Task<List<ClassModuleResponse>> GetClassModules();

        public Task<ClassModuleResponse> Create(ClassModuleRequest request);

        public Task<ClassModuleResponse> Delete(Guid id);

        public Task<ClassModuleResponse> Update(Guid id, ClassModuleRequest request);
    }
}
