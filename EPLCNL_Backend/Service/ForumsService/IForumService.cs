using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace Service.ForumsService
{
    public interface IForumService
    {
        public Task<List<ForumResponse>> GetForums();

        public Task<ForumResponse> Create(ForumRequest request);

        public Task<ForumResponse> Delete(Guid id);

        public Task<ForumResponse> Update(Guid id, ForumRequest request);
    }
}
