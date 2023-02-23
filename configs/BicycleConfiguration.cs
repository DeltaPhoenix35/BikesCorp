using BikesTest.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BikesTest.Models.Bicycle;

namespace BikesTest.configs
{
    public class BicycleConfiguration : IEntityTypeConfiguration<Bicycle>
    {
        public void Configure(EntityTypeBuilder<Bicycle> builder)
        {
            builder.HasKey(o => o.id);

            builder.HasOne(o => o.bicycleType)
                   .WithMany(t => t.bicycles)
                   .HasForeignKey(o => o.bicycleType_Id)
                   .OnDelete(DeleteBehavior.Cascade);

            //builder.HasOne(o => o.bicycleContract)
            //       .WithOne(t => t.bicycle)
            //       .HasForeignKey<BicycleContract>(t => t.bicycle_Id)
            //       .OnDelete(DeleteBehavior.NoAction);

            builder.HasData(
                new Bicycle
            {
                id = 1,
                bicycleType_Id = 1,
                size = BicycleSizes.S,
                purchasePrice = 15000
            }, new Bicycle
            {
                id = 2,
                bicycleType_Id = 1,
                size = BicycleSizes.S,
                purchasePrice = 17000
            }
            );
        }
    }
}
