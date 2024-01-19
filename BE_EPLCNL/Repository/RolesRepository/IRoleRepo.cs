﻿using BusinessObject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.RolesRepository
{
    public interface IRoleRepo
    {
        Task<IEnumerable<Role>> GetAll();
    }
}
