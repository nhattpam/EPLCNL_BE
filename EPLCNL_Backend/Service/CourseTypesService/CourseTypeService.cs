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

namespace Service.CourseTypesService
{
    public class CourseTypeService : ICourseTypeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public CourseTypeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<CourseTypeResponse>> GetCourseTypes()
        {

            var list = await _unitOfWork.Repository<CourseType>().GetAll()
                                            .ProjectTo<CourseTypeResponse>(_mapper.ConfigurationProvider)
                                            .ToListAsync();
            return list;
        }

        public async Task<CourseTypeResponse> Create(CourseTypeRequest request)
        {
            try
            {
                var courseType = _mapper.Map<CourseTypeRequest, CourseType>(request);
                courseType.Id = Guid.NewGuid();
                await _unitOfWork.Repository<CourseType>().InsertAsync(courseType);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<CourseType, CourseTypeResponse>(courseType);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<CourseTypeResponse> Delete(Guid id)
        {
            try
            {
                CourseType courseType = null;
                courseType = _unitOfWork.Repository<CourseType>()
                    .Find(p => p.Id == id);
                if (courseType == null)
                {
                    throw new Exception("Bi trung id");
                }
                await _unitOfWork.Repository<CourseType>().HardDeleteGuid(courseType.Id);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<CourseType, CourseTypeResponse>(courseType);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CourseTypeResponse> Update(Guid id, CourseTypeRequest request)
        {
            try
            {
                CourseType courseType = _unitOfWork.Repository<CourseType>()
                            .Find(x => x.Id == id);
                if (courseType == null)
                {
                    throw new Exception();
                }
                courseType = _mapper.Map(request, courseType);

                await _unitOfWork.Repository<CourseType>().UpdateDetached(courseType);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<CourseType, CourseTypeResponse>(courseType);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
