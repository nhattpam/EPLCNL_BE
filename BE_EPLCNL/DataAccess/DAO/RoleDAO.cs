using BusinessObject.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DAO
{
    public class RoleDAO
    {
        
            private static RoleDAO instance = null;
            private static readonly object instanceLock = new object();

            private RoleDAO()
            {
            }

            public static RoleDAO Instance
            {
                get
                {
                    lock (instanceLock)
                    {
                        if (instance == null)
                        {
                            instance = new RoleDAO();
                        }

                        return instance;
                    }
                }
            }

            public async Task<IEnumerable<Role>> GetAll()
            {
                var context = new EPLCNLContext();
                return await context.Roles.ToListAsync();
            }

        }
    
}
