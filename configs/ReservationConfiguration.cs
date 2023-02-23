using BikesTest.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikesTest.configs
{
    public class ReservationConfiguration : IEntityTypeConfiguration<Reservation>
    {
        public void Configure(EntityTypeBuilder<Reservation> builder)
        {
            builder.HasKey(o => o.id);

            builder.Property(o => o.reservationDate).IsRequired();
            builder.Property(o => o.expectedReturnDate).IsRequired();

            builder.Property(o => o.customer_Id).IsRequired();
            builder.Property(o => o.bicycle_Id).IsRequired();

            builder.HasOne(o => o.coupon)
                   .WithOne(t => t.reservation)
                   .HasForeignKey<Reservation>(t => t.coupon_Id)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(o => o.bicycle)
                   .WithMany(p => p.reservations)
                   .HasForeignKey(o => o.bicycle_Id)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(o => o.customer)
                   .WithMany(p => p.reservations)
                   .HasForeignKey(o => o.customer_Id)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
