﻿using AutoMapper;
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

namespace Service.CoursesService
{
    public class CourseService : ICourseService
    {
        private readonly IUnitOfWork _unitOfWork;
        private IMapper _mapper;
        public CourseService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<List<CourseResponse>> GetCourses()
        {

            var list = await _unitOfWork.Repository<Course>().GetAll()
                                            .ProjectTo<CourseResponse>(_mapper.ConfigurationProvider)
                                            .ToListAsync();
            return list;
        }

        public async Task<CourseResponse> Create(CourseRequest request)
        {
            try
            {
                var course = _mapper.Map<CourseRequest, Course>(request);
                course.Id = Guid.NewGuid();
                await _unitOfWork.Repository<Course>().InsertAsync(course);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<Course, CourseResponse>(course);
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public async Task<CourseResponse> Delete(Guid id)
        {
            try
            {
                Course course = null;
                course = _unitOfWork.Repository<Course>()
                    .Find(p => p.Id == id);
                if (course == null)
                {
                    throw new Exception("Bi trung id");
                }
                await _unitOfWork.Repository<Course>().HardDeleteGuid(course.Id);
                await _unitOfWork.CommitAsync();
                return _mapper.Map<Course, CourseResponse>(course);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public async Task<CourseResponse> Update(Guid id, CourseRequest request)
        {
            try
            {
                Course course = _unitOfWork.Repository<Course>()
                            .Find(x => x.Id == id);
                if (course == null)
                {
                    throw new Exception();
                }
                course = _mapper.Map(request, course);

                await _unitOfWork.Repository<Course>().UpdateDetached(course);
                await _unitOfWork.CommitAsync();

                return _mapper.Map<Course, CourseResponse>(course);
            }

            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
