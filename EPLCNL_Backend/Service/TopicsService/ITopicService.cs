using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace Service.TopicsService
{
    public interface ITopicService
    {
        public Task<List<TopicResponse>> GetAll();

        public Task<TopicResponse> Get(Guid id);

        public Task<List<MaterialResponse>> GetAllMaterialsByClassTopic(Guid id);
        public Task<List<QuizResponse>> GetAllQuizzesByClassTopic(Guid id);
        public Task<List<AssignmentResponse>> GetAllAssignmentsByClassTopic(Guid id);

        public Task<TopicResponse> Create(TopicRequest request);

        public Task<TopicResponse> Delete(Guid id);

        public Task<TopicResponse> Update(Guid id, TopicRequest request);
    }
}
