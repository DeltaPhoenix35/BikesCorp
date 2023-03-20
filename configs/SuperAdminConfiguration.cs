using BikesTest.Models;
using BikesTest.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikesTest.configs
{
    public class SuperAdminConfiguration : IEntityTypeConfiguration<SuperAdmin>
    {

        public void Configure(EntityTypeBuilder<SuperAdmin> builder)
        {
            builder.HasKey(o => o.id);

            builder.HasOne(o => o.user)
                   .WithOne(p => p.superAdmin)
                   .HasForeignKey<SuperAdmin>(o => o.user_id)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(new SuperAdmin
            {
                id = 1,
                user_id = 4,
            });
        }
    }
}
