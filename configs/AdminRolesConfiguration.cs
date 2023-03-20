using BikesTest.Models;
using BikesTest.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BikesTest.Models.Admin;
using static BikesTest.Models.AdminRoles;

namespace BikesTest.configs
{
    public class AdminRolesConfiguration : IEntityTypeConfiguration<AdminRoles>
    {

        public void Configure(EntityTypeBuilder<AdminRoles> builder)
        {
            builder.HasKey(o => o.id);

            //builder.HasMany(o => o.admins)
            //       .WithMany(p => p.roles);

            builder.HasData(new AdminRoles
            {
                id = 1,
                role = Roles.Admins,
            });

            builder.HasData(new AdminRoles
            {
                id = 2,
                role = Roles.Bicycles,
            });

            builder.HasData(new AdminRoles
            {
                id = 3,
                role = Roles.Customers,
            });

            builder.HasData(new AdminRoles
            {
                id = 4,
                role = Roles.Reservations,
            });

            builder.HasData(new AdminRoles
            {
                id = 5,
                role = Roles.Transactions,
            });

            builder.HasData(new AdminRoles
            {
                id = 6,
                role = Roles.StoreTerminal,
            });

        }
    }
}
