using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace Service.LearnersService
{
    public interface ILearnerService
    {
        public Task<List<LearnerResponse>> GetLearners();

        public Task<LearnerResponse> Create(LearnerRequest request);

        public Task<LearnerResponse> Delete(Guid id);

        public Task<LearnerResponse> Update(Guid id, LearnerRequest request);
    }
}
