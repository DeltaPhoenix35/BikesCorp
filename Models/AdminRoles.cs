using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikesTest.Models
{
    public class AdminRoles
    {
        public enum Roles
        {
            Transactions,
            Reservations,
            Customers,
            Admins,
            StoreTerminal,
            Bicycles,
        }

        public int id { get; set; }

        public Roles role { get; set; }

        public List<Admin> admins { get; set; }
    }
}
