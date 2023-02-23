using BikesTest.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikesTest.configs
{
    public class PricingSchemeConfiguration : IEntityTypeConfiguration<PricingScheme>
    {
        public void Configure(EntityTypeBuilder<PricingScheme> builder)
        {
            builder.HasKey(o => o.id);

            //builder.HasOne(o => o.bicycleType)
            //       .WithOne(t => t.pricing)
            //       .HasForeignKey<PricingScheme>(t => t.bicycleType_Id)
            //       .OnDelete(DeleteBehavior.Cascade);

            builder.HasData(new PricingScheme
            {
                id = 1,
                perHour = 25,
                per1Day = 125,
                per2Days = 225,
                per3Days = 325,
                per4Days = 425,
                per5Days = 525,
                perExtraDay = 125,
                bicycleType_Id = 1
            }) ;
        }
    }
}
