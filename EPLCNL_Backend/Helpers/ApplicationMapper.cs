using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Data.Models;
using ViewModel.RequestModel;
using ViewModel.ResponseModel;

namespace Helper
{
    public class ApplicationMapper : Profile
    {
        public ApplicationMapper()
        {
            CreateMap<Role,RoleRequest>().ReverseMap();
            CreateMap<Role,RoleResponse>().ReverseMap();
            CreateMap<Category, CategoryRequest>().ReverseMap();
            CreateMap<Category, CategoryResponse>().ReverseMap();
            CreateMap<Center, CenterRequest>().ReverseMap();
            CreateMap<Center, CenterResponse>().ReverseMap();
            CreateMap<Certificate, CertificateRequest>().ReverseMap();
            CreateMap<Certificate, CertificateResponse>().ReverseMap();
            CreateMap<CertificateCourse, CertificateCourseRequest>().ReverseMap();
            CreateMap<CertificateCourse, CertificateCourseResponse>().ReverseMap();
            CreateMap<ClassLesson, ClassLessonRequest>().ReverseMap();
            CreateMap<ClassLesson, ClassLessonResponse>().ReverseMap();
            CreateMap<ClassMaterial, ClassMaterialRequest>().ReverseMap();
            CreateMap<ClassMaterial, ClassMaterialResponse>().ReverseMap();
            CreateMap<ClassModule, ClassModuleRequest>().ReverseMap();
            CreateMap<ClassModule, ClassModuleResponse>().ReverseMap();
            CreateMap<ClassPractice, ClassPracticeRequest>().ReverseMap();
            CreateMap<ClassPractice, ClassPracticeResponse>().ReverseMap();
            CreateMap<ClassTopic, ClassTopicRequest>().ReverseMap();
            CreateMap<ClassTopic, ClassTopicResponse>().ReverseMap();
            CreateMap<ClassType, ClassTypeRequest>().ReverseMap();
            CreateMap<ClassType, ClassTypeResponse>().ReverseMap();
            CreateMap<Course, CourseRequest>().ReverseMap();
            CreateMap<Course, CourseResponse>().ReverseMap();
        }
    }
}
