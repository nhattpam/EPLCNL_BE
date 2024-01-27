using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace Service.PaperWorkTypesService
{
    public interface IPaperWorkTypeService
    {
        public Task<List<PaperWorkTypeResponse>> GetAll();

        public Task<PaperWorkTypeResponse> Get(Guid id);

        public Task<PaperWorkTypeResponse> Create(PaperWorkTypeRequest request);

        public Task<PaperWorkTypeResponse> Delete(Guid id);

        public Task<PaperWorkTypeResponse> Update(Guid id, PaperWorkTypeRequest request);
    }
}
