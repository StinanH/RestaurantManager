using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace RestaurantManager.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Restaurants",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Restaurants", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TimeSlots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    isAvaliable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSlots", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Menus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FK_RestaurantId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Menus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Menus_Restaurants_FK_RestaurantId",
                        column: x => x.FK_RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tables",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FK_RestaurantId = table.Column<int>(type: "int", nullable: false),
                    NrOfSeats = table.Column<int>(type: "int", nullable: false),
                    RestaurantId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tables", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tables_Restaurants_RestaurantId",
                        column: x => x.RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Accounts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHashed = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    isAdmin = table.Column<bool>(type: "bit", nullable: false),
                    FK_User = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accounts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Accounts_Users_FK_User",
                        column: x => x.FK_User,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FK_UserId = table.Column<int>(type: "int", nullable: false),
                    FK_RestaurantID = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Restaurants_FK_RestaurantID",
                        column: x => x.FK_RestaurantID,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Users_FK_UserId",
                        column: x => x.FK_UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NrOfPeople = table.Column<int>(type: "int", nullable: false),
                    requestedTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    requestedEndTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Requests = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    FK_TimeslotId = table.Column<int>(type: "int", nullable: false),
                    FK_UserID = table.Column<int>(type: "int", nullable: false),
                    FK_TableId = table.Column<int>(type: "int", nullable: false),
                    FK_RestaurantId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_Restaurants_FK_RestaurantId",
                        column: x => x.FK_RestaurantId,
                        principalTable: "Restaurants",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bookings_Tables_FK_TableId",
                        column: x => x.FK_TableId,
                        principalTable: "Tables",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bookings_TimeSlots_FK_TimeslotId",
                        column: x => x.FK_TimeslotId,
                        principalTable: "TimeSlots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Bookings_Users_FK_UserID",
                        column: x => x.FK_UserID,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "MenuItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FK_MenuId = table.Column<int>(type: "int", nullable: false),
                    FK_RestaurantId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Category = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsAvailable = table.Column<bool>(type: "bit", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false),
                    AmountSold = table.Column<int>(type: "int", nullable: false),
                    OrderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MenuItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MenuItems_Menus_FK_MenuId",
                        column: x => x.FK_MenuId,
                        principalTable: "Menus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_MenuItems_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Restaurants",
                columns: new[] { "Id", "Address", "Description", "Email", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "Vällingbygatan 1, 16266 Vällingby", "Sveriges bästa pizzeria", "PazziPizza@gmail.com", "Pazzi Pizza", "0731111111" },
                    { 2, "Astrakangatan 1, 16552 Hässelby", "Världens bästa Café", "CafeKaffe@gmail.com", "Café Kaffe", "0732222222" }
                });

            migrationBuilder.InsertData(
                table: "Tables",
                columns: new[] { "Id", "FK_RestaurantId", "NrOfSeats", "RestaurantId" },
                values: new object[,]
                {
                    { 1, 1, 2, null },
                    { 2, 1, 4, null },
                    { 3, 1, 6, null }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "David@gmail.com", "David Hedman", "1111111111" },
                    { 2, "Leo@gmail.com", "Leo Horrorwitz", "1111111122" },
                    { 3, "Berend@gmail.com", "Berend Mevius", "1111111133" },
                    { 4, "Siri@gmail.com", "Siri Martinsson", "1111111144" },
                    { 5, "Admin@gmail.com", "Adam Min", "0707070707" },
                    { 6, "User@gmail.com", "Userella De Fault", "0737373737" }
                });

            migrationBuilder.InsertData(
                table: "Accounts",
                columns: new[] { "Id", "Email", "FK_User", "PasswordHashed", "isAdmin" },
                values: new object[,]
                {
                    { 1, "Admin@gmail.com", 5, "$2a$11$X0RBosrovnEc7FC.HY7xEOQsRk9sfRSFmXPgQ5VwXdQftTeInjUP2", true },
                    { 2, "User@gmail.com", 6, "$2a$11$HMwmz4mUOj5Ci5eykZ9w2.WeDE/k90raYtMFROd7aCmTY18HiuH2K", false }
                });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "Id", "FK_RestaurantId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Meny" },
                    { 2, 2, "Meny" },
                    { 3, 2, "Helgmeny" }
                });

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "Id", "AmountSold", "Category", "Description", "FK_MenuId", "FK_RestaurantId", "IsAvailable", "Name", "OrderId", "Price" },
                values: new object[,]
                {
                    { 1, 70, "Pizza", "En sorts pizza.", 1, 1, true, "Ananaspizza", null, 90 },
                    { 2, 90, "Pizza", "En annan pizza.", 1, 1, true, "Bananpizza", null, 80 },
                    { 3, 80, "Pizza", "Också pizza.", 1, 1, true, "Bönpizza", null, 70 },
                    { 4, 40, "Pizza", "En Rund pizza.", 1, 1, true, "Pastapizza", null, 60 },
                    { 5, 40, "Dryck", "Lärre.", 1, 1, true, "Cuba cola", null, 50 },
                    { 6, 50, "Pasta", "Pasta med pålägg", 1, 1, true, "Pastasallad med banan", null, 40 },
                    { 7, 70, "Pasta", "Pasta med annat pålägg", 1, 1, true, "Pastasallad med mint", null, 45 },
                    { 8, 90, "Pasta", "Pasta med oätligt pålägg", 1, 1, true, "Pastasallad med lakrits", null, 45 },
                    { 9, 50, "Pizza", "Pizza med champinjoner", 1, 1, true, "Capritjosan", null, 70 },
                    { 10, 56, "Pizza", "Pizza utan champinjoner", 1, 1, true, "Margareta", null, 71 },
                    { 11, 33, "Dryck", "Brun dryck", 1, 1, true, "Kaffe", null, 10 },
                    { 12, 6, "Dryck", "Halvgenomskinlig dryck", 1, 1, true, "Té", null, 10 },
                    { 13, 4, "Dryck", "Rosa lärre", 1, 1, true, "Hallonsoda", null, 20 },
                    { 14, 4, "Dryck", "Gul lärre", 1, 1, true, "Lemonad", null, 20 },
                    { 15, 30, "Dryck", "Orange lärre", 1, 1, true, "Zingo", null, 15 },
                    { 16, 20, "Dryck", "Annan brun dryck", 1, 1, true, "Cola", null, 15 },
                    { 17, 0, "Dryck", "Halvgenomskinlig dryck", 2, 2, true, "Té", null, 100 },
                    { 18, 0, "Bakelser", "Snurrigt bakverk", 3, 2, true, "Bulle", null, 100 },
                    { 19, 0, "Bakelser", "Rosa bakverk", 3, 2, true, "Hallonpaj med grädde", null, 100 },
                    { 20, 0, "Bakelser", "det är paj", 3, 2, true, "Blåbärspaj med grädde", null, 100 },
                    { 21, 0, "Dryck", "Brun dryck", 3, 2, true, "Kaffe", null, 100 },
                    { 22, 0, "Dryck", "Halvgenomskinlig dryck", 3, 2, true, "Té", null, 100 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_FK_User",
                table: "Accounts",
                column: "FK_User",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_FK_RestaurantId",
                table: "Bookings",
                column: "FK_RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_FK_TableId",
                table: "Bookings",
                column: "FK_TableId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_FK_TimeslotId",
                table: "Bookings",
                column: "FK_TimeslotId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_FK_UserID",
                table: "Bookings",
                column: "FK_UserID");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_FK_MenuId",
                table: "MenuItems",
                column: "FK_MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_MenuItems_OrderId",
                table: "MenuItems",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_Menus_FK_RestaurantId",
                table: "Menus",
                column: "FK_RestaurantId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_FK_RestaurantID",
                table: "Orders",
                column: "FK_RestaurantID");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_FK_UserId",
                table: "Orders",
                column: "FK_UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Tables_RestaurantId",
                table: "Tables",
                column: "RestaurantId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accounts");

            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "MenuItems");

            migrationBuilder.DropTable(
                name: "Tables");

            migrationBuilder.DropTable(
                name: "TimeSlots");

            migrationBuilder.DropTable(
                name: "Menus");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Restaurants");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
