using BikesTest.configs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BikesTest.Models;

namespace BikesTest.Models
{
    public class Context : DbContext
    {
        public Context(DbContextOptions<Context> options) : base(options)
        {

        }
        public DbSet<User> Users { set; get; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Bicycle> Bicycles { get; set; }
        public DbSet<BicycleType> bicycleTypes { get; set; }
        public DbSet<BicycleContract> BicycleContracts { get; set; }
        public DbSet<PricingScheme> pricingSchemes { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<CouponType> CouponTypes { get; set; }
        public DbSet<Coupon> Coupons { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<SubscriptionPlan> SubscriptionPlans { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BicycleTypeConfiguration());
            modelBuilder.ApplyConfiguration(new PricingSchemeConfiguration());
            modelBuilder.ApplyConfiguration(new BicycleConfiguration());
            modelBuilder.ApplyConfiguration(new BicycleContractConfiguration());
            modelBuilder.ApplyConfiguration(new CustomerConfiguration());
            modelBuilder.ApplyConfiguration(new CouponTypeConfiguration());
            modelBuilder.ApplyConfiguration(new CouponConfiguration());
            modelBuilder.ApplyConfiguration(new SubscriptionConfiguration());
            modelBuilder.ApplyConfiguration(new SubscriptionPlanConfiguration());
            modelBuilder.ApplyConfiguration(new TransactionConfiguration());
            modelBuilder.ApplyConfiguration(new ReservationConfiguration());
            modelBuilder.ApplyConfiguration(new UserConfiguration());
            modelBuilder.ApplyConfiguration(new AdminConfiguration());
            modelBuilder.ApplyConfiguration(new LocationConfiguration()); 
        }
    }
}
