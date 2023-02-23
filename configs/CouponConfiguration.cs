using BikesTest.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikesTest.configs
{
    public class CouponConfiguration : IEntityTypeConfiguration<Coupon>
    {
        public void Configure(EntityTypeBuilder<Coupon> builder)
        {
            builder.HasKey(o => o.id);

            builder.HasOne(o => o.customer)
                   .WithMany(t => t.coupons)
                   .HasForeignKey(o => o.customer_Id)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(o => o.couponType)
                   .WithMany(t => t.coupons)
                   .HasForeignKey(o => o.couponType_Id)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
