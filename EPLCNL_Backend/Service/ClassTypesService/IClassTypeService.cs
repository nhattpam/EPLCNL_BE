using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace Service.ClassTypesService
{
    public interface IClassTypeService
    {
        public Task<List<ClassTypeResponse>> GetAll();

        public Task<ClassTypeResponse> Create(ClassTypeRequest request);

        public Task<ClassTypeResponse> Delete(Guid id);

        public Task<ClassTypeResponse> Update(Guid id, ClassTypeRequest request);
    }
}
