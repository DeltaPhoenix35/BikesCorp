using BikesTest.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikesTest.configs
{
    public class BicycleContractConfiguration : IEntityTypeConfiguration<BicycleContract>
    {
        public void Configure(EntityTypeBuilder<BicycleContract> builder)
        {
            builder.HasKey(o => o.id);

            builder.HasOne(o => o.bicycle)
                   .WithOne()
                   .HasForeignKey<BicycleContract>(o => o.bicycle_Id)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(o => o.customer)
                   .WithMany(t => t.bicycleContracts)
                   .HasForeignKey(o => o.customer_Id)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
