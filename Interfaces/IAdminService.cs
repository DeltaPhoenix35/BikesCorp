using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BikesTest.Models.AdminRoles;

namespace BikesTest.Interfaces
{
    public interface IAdminService<T> : IUserService<T>
    {
        public List<T> GetByRoles(List<Roles> roles);

    }
}
