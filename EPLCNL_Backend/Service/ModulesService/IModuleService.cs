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
        public Task<List<ModuleResponse>> GetAll();
        public Task<ModuleResponse> Get(Guid id);
        public Task<List<LessonResponse>> GetAllLessonsByModule(Guid id);
        public Task<List<AssignmentResponse>> GetAllAssignmentsByModule(Guid id);
        public Task<List<QuizResponse>> GetAllQuizzesByModule(Guid id);


        public Task<ModuleResponse> Create(ModuleRequest request);

        public Task<ModuleResponse> Delete(Guid id);

        public Task<ModuleResponse> Update(Guid id, ModuleRequest request);
    }
}
