// <auto-generated />
using System;
using BikesTest.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace BikesTest.Migrations
{
    [DbContext(typeof(Context))]
    [Migration("20230223144612_subscriptions_1.5")]
    partial class subscriptions_15
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.7")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("BikesTest.Models.Admin", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("isCurrentlyLogged")
                        .HasColumnType("bit");

                    b.Property<bool>("isSuspended")
                        .HasColumnType("bit");

                    b.Property<int?>("user_id")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("user_id")
                        .IsUnique()
                        .HasFilter("[user_id] IS NOT NULL");

                    b.ToTable("Admins");

                    b.HasData(
                        new
                        {
                            id = 1,
                            isCurrentlyLogged = false,
                            isSuspended = false,
                            user_id = 2
                        });
                });

            modelBuilder.Entity("BikesTest.Models.Bicycle", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("aquisutionDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("bicycleType_Id")
                        .HasColumnType("int");

                    b.Property<double>("earningsToDate")
                        .HasColumnType("float");

                    b.Property<bool>("isConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("isCurrentlyRented")
                        .HasColumnType("bit");

                    b.Property<bool>("isReserved")
                        .HasColumnType("bit");

                    b.Property<DateTime?>("lastCheckupDate")
                        .HasColumnType("datetime2");

                    b.Property<double>("purchasePrice")
                        .HasColumnType("float");

                    b.Property<int>("size")
                        .HasColumnType("int");

                    b.Property<int>("timesRented")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("bicycleType_Id");

                    b.ToTable("Bicycles");

                    b.HasData(
                        new
                        {
                            id = 1,
                            aquisutionDate = new DateTime(2023, 2, 23, 0, 0, 0, 0, DateTimeKind.Local),
                            bicycleType_Id = 1,
                            earningsToDate = 0.0,
                            isConfirmed = true,
                            isCurrentlyRented = false,
                            isReserved = false,
                            purchasePrice = 15000.0,
                            size = 0,
                            timesRented = 0
                        },
                        new
                        {
                            id = 2,
                            aquisutionDate = new DateTime(2023, 2, 23, 0, 0, 0, 0, DateTimeKind.Local),
                            bicycleType_Id = 1,
                            earningsToDate = 0.0,
                            isConfirmed = true,
                            isCurrentlyRented = false,
                            isReserved = false,
                            purchasePrice = 17000.0,
                            size = 0,
                            timesRented = 0
                        });
                });

            modelBuilder.Entity("BikesTest.Models.BicycleContract", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int?>("BicycleTypeid")
                        .HasColumnType("int");

                    b.Property<int?>("bicycle_Id")
                        .HasColumnType("int");

                    b.Property<int>("customer_Id")
                        .HasColumnType("int");

                    b.Property<bool>("isActive")
                        .HasColumnType("bit");

                    b.Property<bool>("isDenied")
                        .HasColumnType("bit");

                    b.Property<string>("moreInfo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("refusalInformation")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("BicycleTypeid");

                    b.HasIndex("bicycle_Id")
                        .IsUnique()
                        .HasFilter("[bicycle_Id] IS NOT NULL");

                    b.HasIndex("customer_Id");

                    b.ToTable("BicycleContracts");
                });

            modelBuilder.Entity("BikesTest.Models.BicycleType", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("pricingScheme_Id")
                        .HasColumnType("int");

                    b.Property<int>("reduction")
                        .HasColumnType("int");

                    b.Property<string>("typeName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("pricingScheme_Id")
                        .IsUnique();

                    b.ToTable("bicycleTypes");

                    b.HasData(
                        new
                        {
                            id = 1,
                            description = "No particular description, the default bike.",
                            pricingScheme_Id = 1,
                            reduction = 0,
                            typeName = "Default"
                        });
                });

            modelBuilder.Entity("BikesTest.Models.Coupon", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("couponType_Id")
                        .HasColumnType("int");

                    b.Property<int>("customer_Id")
                        .HasColumnType("int");

                    b.Property<DateTime>("expiringDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("isDeleted")
                        .HasColumnType("bit");

                    b.Property<bool>("isExpired")
                        .HasColumnType("bit");

                    b.Property<bool>("isReserved")
                        .HasColumnType("bit");

                    b.Property<bool>("isUsed")
                        .HasColumnType("bit");

                    b.Property<int?>("reservation_Id")
                        .HasColumnType("int");

                    b.Property<int?>("transaction_Id")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("couponType_Id");

                    b.HasIndex("customer_Id");

                    b.ToTable("Coupons");
                });

            modelBuilder.Entity("BikesTest.Models.CouponType", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("isDeleted")
                        .HasColumnType("bit");

                    b.Property<int>("pointsToRedeem")
                        .HasColumnType("int");

                    b.Property<int>("value")
                        .HasColumnType("int");

                    b.Property<int>("weeksToExpire")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.ToTable("CouponTypes");
                });

            modelBuilder.Entity("BikesTest.Models.Customer", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("isCurrentlyBiking")
                        .HasColumnType("bit");

                    b.Property<int>("numberOfBikesRented")
                        .HasColumnType("int");

                    b.Property<int>("points")
                        .HasColumnType("int");

                    b.Property<decimal>("timeBiked")
                        .HasColumnType("decimal(30,12)");

                    b.Property<int?>("user_id")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("user_id")
                        .IsUnique()
                        .HasFilter("[user_id] IS NOT NULL");

                    b.ToTable("Customers");

                    b.HasData(
                        new
                        {
                            id = 1,
                            isCurrentlyBiking = false,
                            numberOfBikesRented = 0,
                            points = 0,
                            timeBiked = 0m,
                            user_id = 1
                        });
                });

            modelBuilder.Entity("BikesTest.Models.Location", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("timestamp")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Location");
                });

            modelBuilder.Entity("BikesTest.Models.PricingScheme", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("bicycleType_Id")
                        .HasColumnType("int");

                    b.Property<double>("per1Day")
                        .HasColumnType("float");

                    b.Property<double>("per2Days")
                        .HasColumnType("float");

                    b.Property<double>("per3Days")
                        .HasColumnType("float");

                    b.Property<double>("per4Days")
                        .HasColumnType("float");

                    b.Property<double>("per5Days")
                        .HasColumnType("float");

                    b.Property<double>("perExtraDay")
                        .HasColumnType("float");

                    b.Property<double>("perHour")
                        .HasColumnType("float");

                    b.HasKey("id");

                    b.ToTable("pricingSchemes");

                    b.HasData(
                        new
                        {
                            id = 1,
                            bicycleType_Id = 1,
                            per1Day = 125.0,
                            per2Days = 225.0,
                            per3Days = 325.0,
                            per4Days = 425.0,
                            per5Days = 525.0,
                            perExtraDay = 125.0,
                            perHour = 25.0
                        });
                });

            modelBuilder.Entity("BikesTest.Models.Reservation", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("bicycle_Id")
                        .HasColumnType("int");

                    b.Property<int?>("coupon_Id")
                        .HasColumnType("int");

                    b.Property<int>("customer_Id")
                        .HasColumnType("int");

                    b.Property<DateTime>("expectedReturnDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("isDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("reservationDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("subscription_Id")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("bicycle_Id");

                    b.HasIndex("coupon_Id")
                        .IsUnique()
                        .HasFilter("[coupon_Id] IS NOT NULL");

                    b.HasIndex("customer_Id");

                    b.HasIndex("subscription_Id");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("BikesTest.Models.Subscription", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("bicycleType_Id")
                        .HasColumnType("int");

                    b.Property<int?>("bicycleTypeid")
                        .HasColumnType("int");

                    b.Property<DateTime>("creationDate")
                        .HasColumnType("datetime2");

                    b.Property<int>("customer_Id")
                        .HasColumnType("int");

                    b.Property<bool>("isActive")
                        .HasColumnType("bit");

                    b.Property<int>("subscriptionPlan_Id")
                        .HasColumnType("int");

                    b.Property<int?>("subscriptionPlanid")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("bicycleTypeid");

                    b.HasIndex("subscriptionPlanid");

                    b.HasIndex("customer_Id", "subscriptionPlan_Id")
                        .IsUnique();

                    b.ToTable("Subscriptions");
                });

            modelBuilder.Entity("BikesTest.Models.SubscriptionDays", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("day")
                        .HasColumnType("int");

                    b.Property<DateTime>("endTime")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("startTime")
                        .HasColumnType("datetime2");

                    b.Property<int>("subscription_Id")
                        .HasColumnType("int");

                    b.HasKey("id");

                    b.HasIndex("subscription_Id");

                    b.ToTable("SubscriptionDays");
                });

            modelBuilder.Entity("BikesTest.Models.SubscriptionPlan", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("isActive")
                        .HasColumnType("bit");

                    b.Property<bool>("isDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<decimal>("price")
                        .HasColumnType("decimal(18,2)");

                    b.HasKey("id");

                    b.ToTable("SubscriptionPlans");
                });

            modelBuilder.Entity("BikesTest.Models.Transaction", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("admin_Id")
                        .HasColumnType("int");

                    b.Property<int>("bicycle_Id")
                        .HasColumnType("int");

                    b.Property<double?>("costOfTransaction")
                        .HasColumnType("float");

                    b.Property<int?>("coupon_Id")
                        .HasColumnType("int");

                    b.Property<int>("customer_Id")
                        .HasColumnType("int");

                    b.Property<decimal?>("durationOfTransaction")
                        .HasColumnType("decimal(30,12)");

                    b.Property<DateTime>("expectedReturnDate")
                        .HasColumnType("datetime2");

                    b.Property<bool>("isDeleted")
                        .HasColumnType("bit");

                    b.Property<DateTime>("rentalDate")
                        .HasColumnType("datetime2");

                    b.Property<DateTime?>("returnDate")
                        .HasColumnType("datetime2");

                    b.Property<int?>("subscription_Id")
                        .HasColumnType("int");

                    b.Property<string>("transactionNum")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("id");

                    b.HasIndex("admin_Id");

                    b.HasIndex("bicycle_Id");

                    b.HasIndex("coupon_Id")
                        .IsUnique()
                        .HasFilter("[coupon_Id] IS NOT NULL");

                    b.HasIndex("customer_Id");

                    b.HasIndex("subscription_Id");

                    b.ToTable("Transactions");
                });

            modelBuilder.Entity("BikesTest.Models.User", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<DateTime>("birthday")
                        .HasColumnType("datetime2");

                    b.Property<string>("email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("firstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("lastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("username")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("id");

                    b.HasIndex("email")
                        .IsUnique()
                        .HasDatabaseName("Ix_unique_email")
                        .HasFilter("[email] IS NOT NULL");

                    b.HasIndex("username")
                        .IsUnique()
                        .HasDatabaseName("Ix_unique_username")
                        .HasFilter("[username] IS NOT NULL");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            id = 1,
                            birthday = new DateTime(2000, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            email = "test1@test.com",
                            firstName = "test1",
                            lastName = "test1",
                            password = "zHDskD+YjUjaNyHd1qdXodir9IVM7WCFc0bs85fsMNI=",
                            username = "test1"
                        },
                        new
                        {
                            id = 2,
                            birthday = new DateTime(2000, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            email = "admin@admin.com",
                            firstName = "admin",
                            lastName = "admin",
                            password = "1UzrWHA24VbkJZO+JMC9EDNX92/fgrKOBKd0Vq0F0ho=",
                            username = "admin"
                        });
                });

            modelBuilder.Entity("BikesTest.Models.Admin", b =>
                {
                    b.HasOne("BikesTest.Models.User", "user")
                        .WithOne("admin")
                        .HasForeignKey("BikesTest.Models.Admin", "user_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("user");
                });

            modelBuilder.Entity("BikesTest.Models.Bicycle", b =>
                {
                    b.HasOne("BikesTest.Models.BicycleType", "bicycleType")
                        .WithMany("bicycles")
                        .HasForeignKey("bicycleType_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("bicycleType");
                });

            modelBuilder.Entity("BikesTest.Models.BicycleContract", b =>
                {
                    b.HasOne("BikesTest.Models.BicycleType", null)
                        .WithMany("bicycleContracts")
                        .HasForeignKey("BicycleTypeid");

                    b.HasOne("BikesTest.Models.Bicycle", "bicycle")
                        .WithOne()
                        .HasForeignKey("BikesTest.Models.BicycleContract", "bicycle_Id")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("BikesTest.Models.Customer", "customer")
                        .WithMany("bicycleContracts")
                        .HasForeignKey("customer_Id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.Navigation("bicycle");

                    b.Navigation("customer");
                });

            modelBuilder.Entity("BikesTest.Models.BicycleType", b =>
                {
                    b.HasOne("BikesTest.Models.PricingScheme", "pricing")
                        .WithOne("bicycleType")
                        .HasForeignKey("BikesTest.Models.BicycleType", "pricingScheme_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("pricing");
                });

            modelBuilder.Entity("BikesTest.Models.Coupon", b =>
                {
                    b.HasOne("BikesTest.Models.CouponType", "couponType")
                        .WithMany("coupons")
                        .HasForeignKey("couponType_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("BikesTest.Models.Customer", "customer")
                        .WithMany("coupons")
                        .HasForeignKey("customer_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("couponType");

                    b.Navigation("customer");
                });

            modelBuilder.Entity("BikesTest.Models.Customer", b =>
                {
                    b.HasOne("BikesTest.Models.User", "user")
                        .WithOne("customer")
                        .HasForeignKey("BikesTest.Models.Customer", "user_id")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.Navigation("user");
                });

            modelBuilder.Entity("BikesTest.Models.Reservation", b =>
                {
                    b.HasOne("BikesTest.Models.Bicycle", "bicycle")
                        .WithMany("reservations")
                        .HasForeignKey("bicycle_Id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("BikesTest.Models.Coupon", "coupon")
                        .WithOne("reservation")
                        .HasForeignKey("BikesTest.Models.Reservation", "coupon_Id")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("BikesTest.Models.Customer", "customer")
                        .WithMany("reservations")
                        .HasForeignKey("customer_Id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("BikesTest.Models.Subscription", "subscription")
                        .WithMany("reservations")
                        .HasForeignKey("subscription_Id")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("bicycle");

                    b.Navigation("coupon");

                    b.Navigation("customer");

                    b.Navigation("subscription");
                });

            modelBuilder.Entity("BikesTest.Models.Subscription", b =>
                {
                    b.HasOne("BikesTest.Models.BicycleType", "bicycleType")
                        .WithMany()
                        .HasForeignKey("bicycleTypeid");

                    b.HasOne("BikesTest.Models.Customer", "customer")
                        .WithMany("subscriptions")
                        .HasForeignKey("customer_Id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("BikesTest.Models.SubscriptionPlan", "subscriptionPlan")
                        .WithMany()
                        .HasForeignKey("subscriptionPlanid");

                    b.Navigation("bicycleType");

                    b.Navigation("customer");

                    b.Navigation("subscriptionPlan");
                });

            modelBuilder.Entity("BikesTest.Models.SubscriptionDays", b =>
                {
                    b.HasOne("BikesTest.Models.SubscriptionPlan", "subscription")
                        .WithMany("subscriptionDays")
                        .HasForeignKey("subscription_Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("subscription");
                });

            modelBuilder.Entity("BikesTest.Models.Transaction", b =>
                {
                    b.HasOne("BikesTest.Models.Admin", "admin")
                        .WithMany("transactions")
                        .HasForeignKey("admin_Id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("BikesTest.Models.Bicycle", "bicycle")
                        .WithMany("transactions")
                        .HasForeignKey("bicycle_Id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("BikesTest.Models.Coupon", "coupon")
                        .WithOne("transaction")
                        .HasForeignKey("BikesTest.Models.Transaction", "coupon_Id")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.HasOne("BikesTest.Models.Customer", "customer")
                        .WithMany("transactions")
                        .HasForeignKey("customer_Id")
                        .OnDelete(DeleteBehavior.NoAction)
                        .IsRequired();

                    b.HasOne("BikesTest.Models.Subscription", "subscription")
                        .WithMany("transactions")
                        .HasForeignKey("subscription_Id")
                        .OnDelete(DeleteBehavior.NoAction);

                    b.Navigation("admin");

                    b.Navigation("bicycle");

                    b.Navigation("coupon");

                    b.Navigation("customer");

                    b.Navigation("subscription");
                });

            modelBuilder.Entity("BikesTest.Models.Admin", b =>
                {
                    b.Navigation("transactions");
                });

            modelBuilder.Entity("BikesTest.Models.Bicycle", b =>
                {
                    b.Navigation("reservations");

                    b.Navigation("transactions");
                });

            modelBuilder.Entity("BikesTest.Models.BicycleType", b =>
                {
                    b.Navigation("bicycleContracts");

                    b.Navigation("bicycles");
                });

            modelBuilder.Entity("BikesTest.Models.Coupon", b =>
                {
                    b.Navigation("reservation");

                    b.Navigation("transaction");
                });

            modelBuilder.Entity("BikesTest.Models.CouponType", b =>
                {
                    b.Navigation("coupons");
                });

            modelBuilder.Entity("BikesTest.Models.Customer", b =>
                {
                    b.Navigation("bicycleContracts");

                    b.Navigation("coupons");

                    b.Navigation("reservations");

                    b.Navigation("subscriptions");

                    b.Navigation("transactions");
                });

            modelBuilder.Entity("BikesTest.Models.PricingScheme", b =>
                {
                    b.Navigation("bicycleType");
                });

            modelBuilder.Entity("BikesTest.Models.Subscription", b =>
                {
                    b.Navigation("reservations");

                    b.Navigation("transactions");
                });

            modelBuilder.Entity("BikesTest.Models.SubscriptionPlan", b =>
                {
                    b.Navigation("subscriptionDays");
                });

            modelBuilder.Entity("BikesTest.Models.User", b =>
                {
                    b.Navigation("admin");

                    b.Navigation("customer");
                });
#pragma warning restore 612, 618
        }
    }
}
