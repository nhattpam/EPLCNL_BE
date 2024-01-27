using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace Service.ClassTopicsService
{
    public interface IClassTopicService
    {
        public Task<List<ClassTopicResponse>> GetAll();

        public Task<ClassTopicResponse> Get(Guid id);

        public Task<ClassTopicResponse> Create(ClassTopicRequest request);

        public Task<ClassTopicResponse> Delete(Guid id);

        public Task<ClassTopicResponse> Update(Guid id, ClassTopicRequest request);
    }
}
