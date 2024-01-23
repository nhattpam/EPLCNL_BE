﻿using System;
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
        }
    }
}
