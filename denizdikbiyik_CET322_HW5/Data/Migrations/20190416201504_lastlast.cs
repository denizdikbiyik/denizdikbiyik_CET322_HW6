using Microsoft.EntityFrameworkCore.Migrations;

namespace denizdikbiyik_CET322_HW5.Data.Migrations
{
    public partial class lastlast : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Department",
                table: "AspNetUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Department",
                table: "AspNetUsers",
                nullable: true);
        }
    }
}
