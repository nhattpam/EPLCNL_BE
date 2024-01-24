using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace Service.PaperWorksService
{
    public interface IPaperWorkService
    {
        public Task<List<PaperWorkResponse>> GetAll();

        public Task<PaperWorkResponse> Create(PaperWorkRequest request);

        public Task<PaperWorkResponse> Delete(Guid id);

        public Task<PaperWorkResponse> Update(Guid id, PaperWorkRequest request);
    }
}
