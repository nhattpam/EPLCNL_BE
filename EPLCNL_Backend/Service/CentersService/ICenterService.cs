using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace Service.CentersService
{
    public interface ICenterService
    {
        public Task<List<CenterResponse>> GetCenters();

        public Task<CenterResponse> Create(CenterRequest request);

        public Task<CenterResponse> Delete(Guid id);

        public Task<CenterResponse> Update(Guid id, CenterRequest request);
    }
}
