using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace Service.ClassMaterialsService
{
    public interface IClassMaterialService
    {
        public Task<List<ClassMaterialResponse>> GetClassMaterials();

        public Task<ClassMaterialResponse> Create(ClassMaterialRequest request);

        public Task<ClassMaterialResponse> Delete(Guid id);

        public Task<ClassMaterialResponse> Update(Guid id, ClassMaterialRequest request);
    }
}
