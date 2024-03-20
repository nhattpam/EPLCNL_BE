using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace Service.PeerReviewsService
{
    public interface IPeerReviewService
    {
        public Task<List<PeerReviewResponse>> GetAll();

        public Task<PeerReviewResponse> Get(Guid id);
        public Task<PeerReviewResponse> Create(PeerReviewRequest request);

        public Task<PeerReviewResponse> Delete(Guid id);

        public Task<PeerReviewResponse> Update(Guid id, PeerReviewRequest request);

    }
}
