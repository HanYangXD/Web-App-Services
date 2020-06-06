using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ThAmCo.Events.Data.Migrations
{
    public partial class addStaffStaffBooking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VenueCode",
                schema: "thamco.events",
                table: "Events",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Staffs",
                schema: "thamco.events",
                columns: table => new
                {
                    StaffId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Surname = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    FirstAid = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Staffs", x => x.StaffId);
                });

            migrationBuilder.CreateTable(
                name: "StaffBookings",
                schema: "thamco.events",
                columns: table => new
                {
                    StaffId = table.Column<int>(nullable: false),
                    EventId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StaffBookings", x => new { x.StaffId, x.EventId });
                    table.ForeignKey(
                        name: "FK_StaffBookings_Events_EventId",
                        column: x => x.EventId,
                        principalSchema: "thamco.events",
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StaffBookings_Staffs_StaffId",
                        column: x => x.StaffId,
                        principalSchema: "thamco.events",
                        principalTable: "Staffs",
                        principalColumn: "StaffId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "thamco.events",
                table: "Staffs",
                columns: new[] { "StaffId", "Email", "FirstAid", "FirstName", "Surname" },
                values: new object[] { 1, "hyhy@fcuc.com", true, "Han Yang", "Choo" });

            migrationBuilder.InsertData(
                schema: "thamco.events",
                table: "Staffs",
                columns: new[] { "StaffId", "Email", "FirstAid", "FirstName", "Surname" },
                values: new object[] { 2, "Benben@fcuc.com", false, "Ben Ben", "Law" });

            migrationBuilder.InsertData(
                schema: "thamco.events",
                table: "Staffs",
                columns: new[] { "StaffId", "Email", "FirstAid", "FirstName", "Surname" },
                values: new object[] { 3, "Padoru@fcuc.com", true, "Padoru", "Nero" });

            migrationBuilder.InsertData(
                schema: "thamco.events",
                table: "StaffBookings",
                columns: new[] { "StaffId", "EventId" },
                values: new object[,]
                {
                    { 1, 1 },
                    { 1, 2 },
                    { 2, 1 },
                    { 3, 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_StaffBookings_EventId",
                schema: "thamco.events",
                table: "StaffBookings",
                column: "EventId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StaffBookings",
                schema: "thamco.events");

            migrationBuilder.DropTable(
                name: "Staffs",
                schema: "thamco.events");

            migrationBuilder.DropColumn(
                name: "VenueCode",
                schema: "thamco.events",
                table: "Events");
        }
    }
}
