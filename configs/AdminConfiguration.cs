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
    public class AdminConfiguration : IEntityTypeConfiguration<Admin>
    {

        public void Configure(EntityTypeBuilder<Admin> builder)
        {
            builder.HasKey(o => o.id);

            builder.HasOne(o => o.user)
                   .WithOne(p => p.admin)
                   .HasForeignKey<Admin>(o => o.user_id)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(new Admin
            {
                id = 1,
                isCurrentlyLogged = false,
                isSuspended = false,
                user_id = 2,
            });

            builder.HasData(new Admin
            {
                id = 2,
                isCurrentlyLogged = false,
                isSuspended = false,
                user_id = 3,
            });

            builder.HasData(new Admin
            {
                id = 3,
                isCurrentlyLogged = false,
                isSuspended = false,
                user_id = 4
            });
        }
    }
}
