using Microsoft.EntityFrameworkCore.Migrations;

namespace Hotel_Management_System.Migrations
{
    public partial class CustomerUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "checked_out",
                table: "report_data",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "room_booked_by_name",
                table: "new_room",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "checked_in",
                table: "customer_info",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "checked_out",
                table: "report_data");

            migrationBuilder.DropColumn(
                name: "room_booked_by_name",
                table: "new_room");

            migrationBuilder.DropColumn(
                name: "checked_in",
                table: "customer_info");
        }
    }
}
