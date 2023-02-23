using BikesTest.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikesTest.configs
{
    public class CouponTypeConfiguration : IEntityTypeConfiguration<CouponType>
    {
        public void Configure(EntityTypeBuilder<CouponType> builder)
        {
            builder.HasKey(o => o.id);
        }
    }
}
