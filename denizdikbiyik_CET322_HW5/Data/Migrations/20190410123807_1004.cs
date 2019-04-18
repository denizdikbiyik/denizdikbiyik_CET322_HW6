using Microsoft.EntityFrameworkCore.Migrations;

namespace denizdikbiyik_CET322_HW5.Data.Migrations
{
    public partial class _1004 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SchoolNo",
                table: "AspNetUsers",
                maxLength: 10,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SchoolNo",
                table: "AspNetUsers");
        }
    }
}
