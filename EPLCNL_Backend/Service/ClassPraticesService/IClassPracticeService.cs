using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace Service.ClassPraticesService
{
    public interface IClassPracticeService
    {
        public Task<List<ClassPracticeResponse>> GetAll();

        public Task<ClassPracticeResponse> Get(Guid id);

        public Task<ClassPracticeResponse> Create(ClassPracticeRequest request);

        public Task<ClassPracticeResponse> Delete(Guid id);

        public Task<ClassPracticeResponse> Update(Guid id, ClassPracticeRequest request);
    }
}
