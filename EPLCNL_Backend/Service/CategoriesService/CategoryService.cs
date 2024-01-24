using AutoMapper;
using AutoMapper.QueryableExtensions;
using Data.Models;
using Data.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace Service.CategoriesService
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<CategoryResponse>> GetAll()
        {

            var list = await _unitOfWork.Repository<Category>().GetAll()
                                            .ProjectTo<CategoryResponse>(_mapper.ConfigurationProvider)
                                            .ToListAsync();
            return list;
        }

        public async Task<CategoryResponse> Create(CategoryRequest request)
        {
            try
            {
                var category = _mapper.Map<CategoryRequest, Category>(request);
                category.Id = Guid.NewGuid();
                await _unitOfWork.Repository<Category>().InsertAsync(category);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<Category, CategoryResponse>(category);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<CategoryResponse> Delete(Guid id)
        {
            try
            {
                Category Category = null;
                Category = _unitOfWork.Repository<Category>()
                    .Find(p => p.Id == id);
                if (Category == null)
                {
                    throw new Exception("Bi trung id");
                }
                await _unitOfWork.Repository<Category>().HardDeleteGuid(Category.Id);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<Category, CategoryResponse>(Category);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CategoryResponse> Update(Guid id, CategoryRequest request)
        {
            try
            {
                Category Category = _unitOfWork.Repository<Category>()
                            .Find(x => x.Id == id);
                if (Category == null)
                {
                    throw new Exception();
                }
                Category = _mapper.Map(request, Category);

                await _unitOfWork.Repository<Category>().UpdateDetached(Category);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<Category, CategoryResponse>(Category);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }



    }
}
