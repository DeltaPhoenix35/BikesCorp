using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BikesTest.Migrations
{
    public partial class subscriptions_15 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CouponTypes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    value = table.Column<int>(type: "int", nullable: false),
                    weeksToExpire = table.Column<int>(type: "int", nullable: false),
                    pointsToRedeem = table.Column<int>(type: "int", nullable: false),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CouponTypes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    timestamp = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "pricingSchemes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    perHour = table.Column<double>(type: "float", nullable: false),
                    per1Day = table.Column<double>(type: "float", nullable: false),
                    per2Days = table.Column<double>(type: "float", nullable: false),
                    per3Days = table.Column<double>(type: "float", nullable: false),
                    per4Days = table.Column<double>(type: "float", nullable: false),
                    per5Days = table.Column<double>(type: "float", nullable: false),
                    perExtraDay = table.Column<double>(type: "float", nullable: false),
                    bicycleType_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_pricingSchemes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionPlans",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionPlans", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    firstName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    lastName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    username = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    email = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    password = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    birthday = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "bicycleTypes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    typeName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    pricingScheme_Id = table.Column<int>(type: "int", nullable: false),
                    reduction = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bicycleTypes", x => x.id);
                    table.ForeignKey(
                        name: "FK_bicycleTypes_pricingSchemes_pricingScheme_Id",
                        column: x => x.pricingScheme_Id,
                        principalTable: "pricingSchemes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SubscriptionDays",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    subscription_Id = table.Column<int>(type: "int", nullable: false),
                    day = table.Column<int>(type: "int", nullable: false),
                    startTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    endTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubscriptionDays", x => x.id);
                    table.ForeignKey(
                        name: "FK_SubscriptionDays_SubscriptionPlans_subscription_Id",
                        column: x => x.subscription_Id,
                        principalTable: "SubscriptionPlans",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    isCurrentlyLogged = table.Column<bool>(type: "bit", nullable: false),
                    isSuspended = table.Column<bool>(type: "bit", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.id);
                    table.ForeignKey(
                        name: "FK_Admins_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    isCurrentlyBiking = table.Column<bool>(type: "bit", nullable: false),
                    timeBiked = table.Column<decimal>(type: "decimal(30,12)", nullable: false),
                    numberOfBikesRented = table.Column<int>(type: "int", nullable: false),
                    points = table.Column<int>(type: "int", nullable: false),
                    user_id = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.id);
                    table.ForeignKey(
                        name: "FK_Customers_Users_user_id",
                        column: x => x.user_id,
                        principalTable: "Users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Bicycles",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    bicycleType_Id = table.Column<int>(type: "int", nullable: false),
                    size = table.Column<int>(type: "int", nullable: false),
                    isCurrentlyRented = table.Column<bool>(type: "bit", nullable: false),
                    isReserved = table.Column<bool>(type: "bit", nullable: false),
                    isConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    lastCheckupDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    timesRented = table.Column<int>(type: "int", nullable: false),
                    earningsToDate = table.Column<double>(type: "float", nullable: false),
                    aquisutionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    purchasePrice = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bicycles", x => x.id);
                    table.ForeignKey(
                        name: "FK_Bicycles_bicycleTypes_bicycleType_Id",
                        column: x => x.bicycleType_Id,
                        principalTable: "bicycleTypes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Coupons",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    couponType_Id = table.Column<int>(type: "int", nullable: false),
                    expiringDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    customer_Id = table.Column<int>(type: "int", nullable: false),
                    transaction_Id = table.Column<int>(type: "int", nullable: true),
                    reservation_Id = table.Column<int>(type: "int", nullable: true),
                    isUsed = table.Column<bool>(type: "bit", nullable: false),
                    isExpired = table.Column<bool>(type: "bit", nullable: false),
                    isReserved = table.Column<bool>(type: "bit", nullable: false),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coupons", x => x.id);
                    table.ForeignKey(
                        name: "FK_Coupons_CouponTypes_couponType_Id",
                        column: x => x.couponType_Id,
                        principalTable: "CouponTypes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Coupons_Customers_customer_Id",
                        column: x => x.customer_Id,
                        principalTable: "Customers",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Subscriptions",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    subscriptionPlan_Id = table.Column<int>(type: "int", nullable: false),
                    subscriptionPlanid = table.Column<int>(type: "int", nullable: true),
                    bicycleType_Id = table.Column<int>(type: "int", nullable: false),
                    bicycleTypeid = table.Column<int>(type: "int", nullable: true),
                    customer_Id = table.Column<int>(type: "int", nullable: false),
                    creationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subscriptions", x => x.id);
                    table.ForeignKey(
                        name: "FK_Subscriptions_bicycleTypes_bicycleTypeid",
                        column: x => x.bicycleTypeid,
                        principalTable: "bicycleTypes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Subscriptions_Customers_customer_Id",
                        column: x => x.customer_Id,
                        principalTable: "Customers",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Subscriptions_SubscriptionPlans_subscriptionPlanid",
                        column: x => x.subscriptionPlanid,
                        principalTable: "SubscriptionPlans",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BicycleContracts",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    customer_Id = table.Column<int>(type: "int", nullable: false),
                    bicycle_Id = table.Column<int>(type: "int", nullable: true),
                    moreInfo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    refusalInformation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    isActive = table.Column<bool>(type: "bit", nullable: false),
                    isDenied = table.Column<bool>(type: "bit", nullable: false),
                    BicycleTypeid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BicycleContracts", x => x.id);
                    table.ForeignKey(
                        name: "FK_BicycleContracts_Bicycles_bicycle_Id",
                        column: x => x.bicycle_Id,
                        principalTable: "Bicycles",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_BicycleContracts_bicycleTypes_BicycleTypeid",
                        column: x => x.BicycleTypeid,
                        principalTable: "bicycleTypes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BicycleContracts_Customers_customer_Id",
                        column: x => x.customer_Id,
                        principalTable: "Customers",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    bicycle_Id = table.Column<int>(type: "int", nullable: false),
                    customer_Id = table.Column<int>(type: "int", nullable: false),
                    coupon_Id = table.Column<int>(type: "int", nullable: true),
                    reservationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    expectedReturnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    subscription_Id = table.Column<int>(type: "int", nullable: true),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.id);
                    table.ForeignKey(
                        name: "FK_Reservations_Bicycles_bicycle_Id",
                        column: x => x.bicycle_Id,
                        principalTable: "Bicycles",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Reservations_Coupons_coupon_Id",
                        column: x => x.coupon_Id,
                        principalTable: "Coupons",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Reservations_Customers_customer_Id",
                        column: x => x.customer_Id,
                        principalTable: "Customers",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Reservations_Subscriptions_subscription_Id",
                        column: x => x.subscription_Id,
                        principalTable: "Subscriptions",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    transactionNum = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    bicycle_Id = table.Column<int>(type: "int", nullable: false),
                    customer_Id = table.Column<int>(type: "int", nullable: false),
                    admin_Id = table.Column<int>(type: "int", nullable: false),
                    rentalDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    expectedReturnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    returnDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    coupon_Id = table.Column<int>(type: "int", nullable: true),
                    durationOfTransaction = table.Column<decimal>(type: "decimal(30,12)", nullable: true),
                    costOfTransaction = table.Column<double>(type: "float", nullable: true),
                    subscription_Id = table.Column<int>(type: "int", nullable: true),
                    isDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.id);
                    table.ForeignKey(
                        name: "FK_Transactions_Admins_admin_Id",
                        column: x => x.admin_Id,
                        principalTable: "Admins",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Transactions_Bicycles_bicycle_Id",
                        column: x => x.bicycle_Id,
                        principalTable: "Bicycles",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Transactions_Coupons_coupon_Id",
                        column: x => x.coupon_Id,
                        principalTable: "Coupons",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Transactions_Customers_customer_Id",
                        column: x => x.customer_Id,
                        principalTable: "Customers",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_Transactions_Subscriptions_subscription_Id",
                        column: x => x.subscription_Id,
                        principalTable: "Subscriptions",
                        principalColumn: "id");
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "birthday", "email", "firstName", "lastName", "password", "username" },
                values: new object[] { 1, new DateTime(2000, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "test1@test.com", "test1", "test1", "zHDskD+YjUjaNyHd1qdXodir9IVM7WCFc0bs85fsMNI=", "test1" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "id", "birthday", "email", "firstName", "lastName", "password", "username" },
                values: new object[] { 2, new DateTime(2000, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@admin.com", "admin", "admin", "1UzrWHA24VbkJZO+JMC9EDNX92/fgrKOBKd0Vq0F0ho=", "admin" });

            migrationBuilder.InsertData(
                table: "pricingSchemes",
                columns: new[] { "id", "bicycleType_Id", "per1Day", "per2Days", "per3Days", "per4Days", "per5Days", "perExtraDay", "perHour" },
                values: new object[] { 1, 1, 125.0, 225.0, 325.0, 425.0, 525.0, 125.0, 25.0 });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "id", "isCurrentlyLogged", "isSuspended", "user_id" },
                values: new object[] { 1, false, false, 2 });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "id", "isCurrentlyBiking", "numberOfBikesRented", "points", "timeBiked", "user_id" },
                values: new object[] { 1, false, 0, 0, 0m, 1 });

            migrationBuilder.InsertData(
                table: "bicycleTypes",
                columns: new[] { "id", "description", "pricingScheme_Id", "reduction", "typeName" },
                values: new object[] { 1, "No particular description, the default bike.", 1, 0, "Default" });

            migrationBuilder.InsertData(
                table: "Bicycles",
                columns: new[] { "id", "aquisutionDate", "bicycleType_Id", "earningsToDate", "isConfirmed", "isCurrentlyRented", "isReserved", "lastCheckupDate", "purchasePrice", "size", "timesRented" },
                values: new object[] { 1, new DateTime(2023, 2, 23, 0, 0, 0, 0, DateTimeKind.Local), 1, 0.0, true, false, false, null, 15000.0, 0, 0 });

            migrationBuilder.InsertData(
                table: "Bicycles",
                columns: new[] { "id", "aquisutionDate", "bicycleType_Id", "earningsToDate", "isConfirmed", "isCurrentlyRented", "isReserved", "lastCheckupDate", "purchasePrice", "size", "timesRented" },
                values: new object[] { 2, new DateTime(2023, 2, 23, 0, 0, 0, 0, DateTimeKind.Local), 1, 0.0, true, false, false, null, 17000.0, 0, 0 });

            migrationBuilder.CreateIndex(
                name: "IX_Admins_user_id",
                table: "Admins",
                column: "user_id",
                unique: true,
                filter: "[user_id] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BicycleContracts_bicycle_Id",
                table: "BicycleContracts",
                column: "bicycle_Id",
                unique: true,
                filter: "[bicycle_Id] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_BicycleContracts_BicycleTypeid",
                table: "BicycleContracts",
                column: "BicycleTypeid");

            migrationBuilder.CreateIndex(
                name: "IX_BicycleContracts_customer_Id",
                table: "BicycleContracts",
                column: "customer_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Bicycles_bicycleType_Id",
                table: "Bicycles",
                column: "bicycleType_Id");

            migrationBuilder.CreateIndex(
                name: "IX_bicycleTypes_pricingScheme_Id",
                table: "bicycleTypes",
                column: "pricingScheme_Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Coupons_couponType_Id",
                table: "Coupons",
                column: "couponType_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Coupons_customer_Id",
                table: "Coupons",
                column: "customer_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_user_id",
                table: "Customers",
                column: "user_id",
                unique: true,
                filter: "[user_id] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_bicycle_Id",
                table: "Reservations",
                column: "bicycle_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_coupon_Id",
                table: "Reservations",
                column: "coupon_Id",
                unique: true,
                filter: "[coupon_Id] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_customer_Id",
                table: "Reservations",
                column: "customer_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_subscription_Id",
                table: "Reservations",
                column: "subscription_Id");

            migrationBuilder.CreateIndex(
                name: "IX_SubscriptionDays_subscription_Id",
                table: "SubscriptionDays",
                column: "subscription_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_bicycleTypeid",
                table: "Subscriptions",
                column: "bicycleTypeid");

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_customer_Id_subscriptionPlan_Id",
                table: "Subscriptions",
                columns: new[] { "customer_Id", "subscriptionPlan_Id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Subscriptions_subscriptionPlanid",
                table: "Subscriptions",
                column: "subscriptionPlanid");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_admin_Id",
                table: "Transactions",
                column: "admin_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_bicycle_Id",
                table: "Transactions",
                column: "bicycle_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_coupon_Id",
                table: "Transactions",
                column: "coupon_Id",
                unique: true,
                filter: "[coupon_Id] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_customer_Id",
                table: "Transactions",
                column: "customer_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_subscription_Id",
                table: "Transactions",
                column: "subscription_Id");

            migrationBuilder.CreateIndex(
                name: "Ix_unique_email",
                table: "Users",
                column: "email",
                unique: true,
                filter: "[email] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "Ix_unique_username",
                table: "Users",
                column: "username",
                unique: true,
                filter: "[username] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BicycleContracts");

            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "SubscriptionDays");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Bicycles");

            migrationBuilder.DropTable(
                name: "Coupons");

            migrationBuilder.DropTable(
                name: "Subscriptions");

            migrationBuilder.DropTable(
                name: "CouponTypes");

            migrationBuilder.DropTable(
                name: "bicycleTypes");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "SubscriptionPlans");

            migrationBuilder.DropTable(
                name: "pricingSchemes");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
