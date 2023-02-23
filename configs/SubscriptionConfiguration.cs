using BikesTest.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikesTest.configs
{
    public class SubscriptionConfiguration : IEntityTypeConfiguration<Subscription>
    {
        public void Configure(EntityTypeBuilder<Subscription> builder)
        {
            builder.HasKey(o => o.id);

            builder.HasIndex(o => new { o.customer_Id, o.subscriptionPlan_Id })
                   .IsUnique(true);

            builder.HasOne(o => o.customer)
                   .WithMany(o => o.subscriptions)
                   .HasForeignKey(o => o.customer_Id)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(o => o.transactions)
                   .WithOne(o => o.subscription)
                   .HasForeignKey(o => o.subscription_Id)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(o => o.reservations)
                   .WithOne(o => o.subscription)
                   .HasForeignKey(o => o.subscription_Id)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
