using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ThAmCo.Venues.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "thamco.venues");

            migrationBuilder.CreateTable(
                name: "EventTypes",
                schema: "thamco.venues",
                columns: table => new
                {
                    Id = table.Column<string>(fixedLength: true, maxLength: 3, nullable: false),
                    Title = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Venues",
                schema: "thamco.venues",
                columns: table => new
                {
                    Code = table.Column<string>(fixedLength: true, maxLength: 5, nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Capacity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Venues", x => x.Code);
                });

            migrationBuilder.CreateTable(
                name: "Availabilities",
                schema: "thamco.venues",
                columns: table => new
                {
                    Date = table.Column<DateTime>(nullable: false),
                    VenueCode = table.Column<string>(nullable: false),
                    CostPerHour = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Availabilities", x => new { x.Date, x.VenueCode });
                    table.ForeignKey(
                        name: "FK_Availabilities_Venues_VenueCode",
                        column: x => x.VenueCode,
                        principalSchema: "thamco.venues",
                        principalTable: "Venues",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Suitabilities",
                schema: "thamco.venues",
                columns: table => new
                {
                    EventTypeId = table.Column<string>(nullable: false),
                    VenueCode = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suitabilities", x => new { x.EventTypeId, x.VenueCode });
                    table.ForeignKey(
                        name: "FK_Suitabilities_EventTypes_EventTypeId",
                        column: x => x.EventTypeId,
                        principalSchema: "thamco.venues",
                        principalTable: "EventTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Suitabilities_Venues_VenueCode",
                        column: x => x.VenueCode,
                        principalSchema: "thamco.venues",
                        principalTable: "Venues",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                schema: "thamco.venues",
                columns: table => new
                {
                    Reference = table.Column<string>(fixedLength: true, maxLength: 13, nullable: false),
                    EventDate = table.Column<DateTime>(nullable: false),
                    VenueCode = table.Column<string>(nullable: false),
                    WhenMade = table.Column<DateTime>(nullable: false),
                    StaffId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Reference);
                    table.ForeignKey(
                        name: "FK_Reservations_Availabilities_EventDate_VenueCode",
                        columns: x => new { x.EventDate, x.VenueCode },
                        principalSchema: "thamco.venues",
                        principalTable: "Availabilities",
                        principalColumns: new[] { "Date", "VenueCode" },
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                schema: "thamco.venues",
                table: "EventTypes",
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { "CON", "Conference" },
                    { "MET", "Meeting" },
                    { "PTY", "Party" },
                    { "WED", "Wedding" }
                });

            migrationBuilder.InsertData(
                schema: "thamco.venues",
                table: "Venues",
                columns: new[] { "Code", "Capacity", "Description", "Name" },
                values: new object[,]
                {
                    { "CRKHL", 150, "Once the residence of Lord and Lady Crackling, this lavish dwelling remains a prime example of 18th century fine living.", "Crackling Hall" },
                    { "TNDMR", 450, "Refurbished manor house with fully equipped facilities ready to help you have a good time in business or pleasure.", "Tinder Manor" },
                    { "FDLCK", 85, "Rustic pub set in ideallic countryside, the original venue of a notorious local musician and his parrot.", "The Fiddler's Cockatoo" }
                });

            migrationBuilder.InsertData(
                schema: "thamco.venues",
                table: "Availabilities",
                columns: new[] { "Date", "VenueCode", "CostPerHour" },
                values: new object[,]
                {
                    { new DateTime(2018, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "CRKHL", 91.03 },
                    { new DateTime(2018, 11, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "FDLCK", 59.86 },
                    { new DateTime(2018, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "FDLCK", 57.18 },
                    { new DateTime(2018, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "FDLCK", 46.77 },
                    { new DateTime(2018, 10, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "FDLCK", 51.79 },
                    { new DateTime(2019, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "TNDMR", 112.63 },
                    { new DateTime(2019, 1, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "TNDMR", 115.3 },
                    { new DateTime(2019, 1, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "TNDMR", 99.88 },
                    { new DateTime(2018, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "FDLCK", 55.44 },
                    { new DateTime(2019, 1, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "TNDMR", 102.22 },
                    { new DateTime(2019, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "TNDMR", 130.17 },
                    { new DateTime(2019, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "TNDMR", 110.11 },
                    { new DateTime(2019, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "TNDMR", 77.7 },
                    { new DateTime(2018, 12, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "TNDMR", 95.83 },
                    { new DateTime(2018, 12, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "TNDMR", 114.65 },
                    { new DateTime(2018, 12, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "TNDMR", 104.41 },
                    { new DateTime(2018, 12, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "TNDMR", 132.13 },
                    { new DateTime(2019, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "TNDMR", 101.32 },
                    { new DateTime(2018, 12, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "TNDMR", 83.71 },
                    { new DateTime(2018, 11, 23, 0, 0, 0, 0, DateTimeKind.Unspecified), "FDLCK", 58.02 },
                    { new DateTime(2018, 11, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "FDLCK", 32.43 },
                    { new DateTime(2019, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "FDLCK", 52.94 },
                    { new DateTime(2019, 1, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "FDLCK", 53.81 },
                    { new DateTime(2019, 1, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "FDLCK", 44.05 },
                    { new DateTime(2019, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "FDLCK", 35.67 },
                    { new DateTime(2019, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "FDLCK", 33.03 },
                    { new DateTime(2018, 12, 26, 0, 0, 0, 0, DateTimeKind.Unspecified), "FDLCK", 50.39 },
                    { new DateTime(2018, 12, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "FDLCK", 35.85 },
                    { new DateTime(2018, 11, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "FDLCK", 50.63 },
                    { new DateTime(2018, 12, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "FDLCK", 48.59 },
                    { new DateTime(2018, 12, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "FDLCK", 42.3 },
                    { new DateTime(2018, 12, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "FDLCK", 43.72 },
                    { new DateTime(2018, 12, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "FDLCK", 40.11 },
                    { new DateTime(2018, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "FDLCK", 53.84 },
                    { new DateTime(2018, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "FDLCK", 51.48 },
                    { new DateTime(2018, 12, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "FDLCK", 30.91 },
                    { new DateTime(2018, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "FDLCK", 49.28 },
                    { new DateTime(2018, 12, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "FDLCK", 51.56 },
                    { new DateTime(2018, 12, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "TNDMR", 76.81 },
                    { new DateTime(2018, 12, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "TNDMR", 87.87 },
                    { new DateTime(2018, 12, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "TNDMR", 109.15 },
                    { new DateTime(2018, 12, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "CRKHL", 80.66 },
                    { new DateTime(2018, 12, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "CRKHL", 92.32 },
                    { new DateTime(2018, 12, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "CRKHL", 64.03 },
                    { new DateTime(2018, 12, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "CRKHL", 64.02 },
                    { new DateTime(2018, 12, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "CRKHL", 76.14 },
                    { new DateTime(2018, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "CRKHL", 69.36 },
                    { new DateTime(2018, 12, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "CRKHL", 99.44 },
                    { new DateTime(2018, 11, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "CRKHL", 53.12 },
                    { new DateTime(2018, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "CRKHL", 84.98 },
                    { new DateTime(2018, 11, 29, 0, 0, 0, 0, DateTimeKind.Unspecified), "CRKHL", 92.52 },
                    { new DateTime(2018, 11, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "CRKHL", 78.49 },
                    { new DateTime(2018, 11, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "CRKHL", 57.4 },
                    { new DateTime(2018, 11, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "CRKHL", 97.65 },
                    { new DateTime(2018, 11, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "CRKHL", 80.49 },
                    { new DateTime(2018, 11, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "CRKHL", 57.45 },
                    { new DateTime(2018, 11, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "CRKHL", 95.01 },
                    { new DateTime(2018, 11, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "CRKHL", 96.38 },
                    { new DateTime(2018, 11, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "CRKHL", 61.13 },
                    { new DateTime(2018, 11, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "CRKHL", 68.05 },
                    { new DateTime(2018, 12, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "CRKHL", 80.77 },
                    { new DateTime(2018, 12, 22, 0, 0, 0, 0, DateTimeKind.Unspecified), "CRKHL", 91.53 },
                    { new DateTime(2018, 12, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "CRKHL", 62.54 },
                    { new DateTime(2018, 12, 30, 0, 0, 0, 0, DateTimeKind.Unspecified), "CRKHL", 78.43 },
                    { new DateTime(2019, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "CRKHL", 77.64 },
                    { new DateTime(2018, 12, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "TNDMR", 112.88 },
                    { new DateTime(2018, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "TNDMR", 104.76 },
                    { new DateTime(2019, 1, 17, 0, 0, 0, 0, DateTimeKind.Unspecified), "CRKHL", 53.22 },
                    { new DateTime(2019, 1, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), "CRKHL", 74.15 },
                    { new DateTime(2019, 1, 24, 0, 0, 0, 0, DateTimeKind.Unspecified), "CRKHL", 72.34 },
                    { new DateTime(2018, 11, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "TNDMR", 94.67 },
                    { new DateTime(2018, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "TNDMR", 115.89 },
                    { new DateTime(2018, 11, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "TNDMR", 79.26 },
                    { new DateTime(2018, 11, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "TNDMR", 72.07 },
                    { new DateTime(2018, 11, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "TNDMR", 139.55 },
                    { new DateTime(2018, 11, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "TNDMR", 92.69 }
                });

            migrationBuilder.InsertData(
                schema: "thamco.venues",
                table: "Suitabilities",
                columns: new[] { "EventTypeId", "VenueCode" },
                values: new object[,]
                {
                    { "WED", "FDLCK" },
                    { "CON", "CRKHL" },
                    { "WED", "CRKHL" },
                    { "WED", "TNDMR" },
                    { "CON", "TNDMR" },
                    { "MET", "TNDMR" },
                    { "PTY", "CRKHL" },
                    { "PTY", "FDLCK" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Availabilities_VenueCode",
                schema: "thamco.venues",
                table: "Availabilities",
                column: "VenueCode");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_EventDate_VenueCode",
                schema: "thamco.venues",
                table: "Reservations",
                columns: new[] { "EventDate", "VenueCode" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Suitabilities_VenueCode",
                schema: "thamco.venues",
                table: "Suitabilities",
                column: "VenueCode");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservations",
                schema: "thamco.venues");

            migrationBuilder.DropTable(
                name: "Suitabilities",
                schema: "thamco.venues");

            migrationBuilder.DropTable(
                name: "Availabilities",
                schema: "thamco.venues");

            migrationBuilder.DropTable(
                name: "EventTypes",
                schema: "thamco.venues");

            migrationBuilder.DropTable(
                name: "Venues",
                schema: "thamco.venues");
        }
    }
}
