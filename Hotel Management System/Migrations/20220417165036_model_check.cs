using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Hotel_Management_System.Migrations
{
    public partial class model_check : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "expense_data",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    entry_date = table.Column<DateTime>(nullable: false),
                    expense_details = table.Column<string>(nullable: true),
                    expense_cost = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_expense_data", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "report_data",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    invoice_no = table.Column<string>(nullable: true),
                    check_in_time = table.Column<DateTime>(nullable: false),
                    check_out_time = table.Column<DateTime>(nullable: false),
                    customer_name = table.Column<string>(nullable: true),
                    customer_phone = table.Column<long>(nullable: false),
                    customer_from = table.Column<string>(nullable: true),
                    room_no = table.Column<string>(nullable: true),
                    room_price = table.Column<string>(nullable: true),
                    stayed = table.Column<int>(nullable: false),
                    room_total = table.Column<string>(nullable: true),
                    sub_total = table.Column<decimal>(nullable: false),
                    discount = table.Column<decimal>(nullable: false),
                    grand_total = table.Column<decimal>(nullable: false),
                    advance_amount = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_report_data", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "expense_data");

            migrationBuilder.DropTable(
                name: "report_data");
        }
    }
}
