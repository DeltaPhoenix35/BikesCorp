using BikesTest.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikesTest.configs
{
    public class BicycleTypeConfiguration : IEntityTypeConfiguration<BicycleType>
    {
        public void Configure(EntityTypeBuilder<BicycleType> builder)
        {
            builder.HasKey(o => o.id);

            builder.Property(o => o.pricingScheme_Id).IsRequired();

            builder.HasOne(o => o.pricing)
                   .WithOne(t => t.bicycleType)
                   .HasForeignKey<BicycleType>(t => t.pricingScheme_Id)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(o => o.bicycles)
                   .WithOne(t => t.bicycleType)
                   .HasForeignKey(t => t.bicycleType_Id)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasData(new BicycleType
            {
                id = 1,
                typeName = "Default",
                description = "No particular description, the default bike.",
                pricingScheme_Id = 1
            }) ;
        }
    }
}
