using BusinessObject.Models;
using DataAccess.DAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.RolesRepository
{
    public class RoleRepo : IRoleRepo
    {
        public Task<IEnumerable<Role>> GetAll()=> RoleDAO.Instance.GetAll();
    }
}
