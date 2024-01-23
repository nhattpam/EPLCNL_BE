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
            CreateMap<Account,AccountRequest>().ReverseMap();
            CreateMap<Account,AccountResponse>().ReverseMap();
            CreateMap<AccountForum,AccountForumRequest>().ReverseMap();
            CreateMap<AccountForum,AccountForumResponse>().ReverseMap();
            CreateMap<AccountSurvey,AccountSurveyRequest>().ReverseMap();
            CreateMap<AccountSurvey,AccountSurveyResponse>().ReverseMap();
            CreateMap<Assignment,AssignmentRequest>().ReverseMap();
            CreateMap<Assignment,AssignmentResponse>().ReverseMap();
            CreateMap<AssignmentAttempt,AssignmentAttemptRequest>().ReverseMap();
            CreateMap<AssignmentAttempt,AssignmentAttemptResponse>().ReverseMap();
        }
    }
}
