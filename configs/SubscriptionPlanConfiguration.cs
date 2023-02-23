using BikesTest.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BikesTest.configs
{
    public class SubscriptionPlanConfiguration : IEntityTypeConfiguration<SubscriptionPlan>
    {
        public void Configure(EntityTypeBuilder<SubscriptionPlan> builder)
        {
            builder.HasKey(o => o.id);

            builder.HasMany(o => o.subscriptionDays)
                   .WithOne(t => t.subscription)
                   .HasForeignKey(t => t.subscription_Id)
                   .OnDelete(DeleteBehavior.Cascade);

        }
    }
}
