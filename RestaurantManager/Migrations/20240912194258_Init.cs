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
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
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
                    isAvaliable = table.Column<bool>(type: "bit", nullable: false),
                    AmountAvaliable = table.Column<int>(type: "int", nullable: false),
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
                        principalColumn: "Id");
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
                table: "Users",
                columns: new[] { "Id", "Email", "Name", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, "David@gmail.com", "David Hedman", "1111111111" },
                    { 2, "Leo@gmail.com", "Leo Horrorwitz", "1111111122" },
                    { 3, "Berend@gmail.com", "Berend Mevius", "1111111133" },
                    { 4, "Siri@gmail.com", "Siri Martinsson", "1111111144" }
                });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "Id", "FK_RestaurantId", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Meny" },
                    { 2, 1, "Lunchmeny" },
                    { 3, 2, "Meny" },
                    { 4, 2, "Helgmeny" }
                });

            migrationBuilder.InsertData(
                table: "MenuItems",
                columns: new[] { "Id", "AmountAvaliable", "Category", "Description", "FK_MenuId", "FK_RestaurantId", "Name", "OrderId", "isAvaliable" },
                values: new object[,]
                {
                    { 1, 100, "Pizza", "En sorts pizza.", 1, 0, "Ananaspizza", null, true },
                    { 2, 100, "Pizza", "En annan pizza.", 1, 0, "Bananpizza", null, true },
                    { 3, 100, "Pizza", "Också pizza.", 1, 0, "Bönpizza", null, true },
                    { 4, 100, "Pizza", "En Rund pizza.", 1, 0, "Pastapizza", null, true },
                    { 5, 100, "Dryck", "Lärre.", 1, 0, "Cuba cola", null, true },
                    { 6, 100, "Pasta", "Pasta med pålägg", 2, 0, "Pastasallad med banan", null, true },
                    { 7, 100, "Pasta", "Pasta med annat pålägg", 2, 0, "Pastasallad med mint", null, true },
                    { 8, 100, "Pasta", "Pasta med oätligt pålägg", 2, 0, "Pastasallad med lakrits", null, true },
                    { 9, 100, "Pizza", "Pizza med champinjoner", 2, 0, "Capritjosan", null, true },
                    { 10, 100, "Pizza", "Pizza utan champinjoner", 2, 0, "Margareta", null, true },
                    { 11, 100, "Bakelser", "Snurrigt bakverk", 3, 0, "Bulle", null, true },
                    { 12, 100, "Bakelser", "Fyrkantigt bakverk", 3, 0, "Kärleksrutor", null, true },
                    { 13, 100, "Bakelser", "Sfäriskt bakverk", 3, 0, "Chokladboll", null, true },
                    { 14, 100, "Dryck", "Brun dryck", 3, 0, "Kaffe", null, true },
                    { 15, 100, "Dryck", "Halvgenomskinlig dryck", 3, 0, "Té", null, true },
                    { 16, 100, "Bakelser", "Snurrigt bakverk", 4, 0, "Bulle", null, true },
                    { 17, 100, "Bakelser", "Rosa bakverk", 4, 0, "Hallonpaj med grädde", null, true },
                    { 18, 100, "Bakelser", "det är paj", 4, 0, "Blåbärspaj med grädde", null, true },
                    { 19, 100, "Dryck", "Brun dryck", 3, 0, "Kaffe", null, true },
                    { 20, 100, "Dryck", "Halvgenomskinlig dryck", 3, 0, "Té", null, true }
                });

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
