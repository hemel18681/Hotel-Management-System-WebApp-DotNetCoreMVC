using Microsoft.EntityFrameworkCore.Migrations;

namespace Hotel_Management_System.Migrations
{
    public partial class expense_model_and_report_model : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "room_booked_date",
                table: "new_room",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "room_booked_hour",
                table: "new_room",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "room_booked_minute",
                table: "new_room",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "room_booked_date",
                table: "new_room");

            migrationBuilder.DropColumn(
                name: "room_booked_hour",
                table: "new_room");

            migrationBuilder.DropColumn(
                name: "room_booked_minute",
                table: "new_room");
        }
    }
}
