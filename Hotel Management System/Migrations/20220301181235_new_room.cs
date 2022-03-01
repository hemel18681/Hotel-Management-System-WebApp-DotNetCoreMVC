using Microsoft.EntityFrameworkCore.Migrations;

namespace Hotel_Management_System.Migrations
{
    public partial class new_room : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "new_room",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    room_type = table.Column<string>(nullable: false),
                    room_price = table.Column<double>(nullable: false),
                    room_floor = table.Column<int>(nullable: false),
                    room_status = table.Column<bool>(nullable: false),
                    room_no = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_new_room", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "new_room");
        }
    }
}
