using Microsoft.EntityFrameworkCore.Migrations;

namespace denizdikbiyik_CET322_HW5.Data.Migrations
{
    public partial class cetuser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "City",
                table: "Student");

            migrationBuilder.AddColumn<string>(
                name: "CetUserId",
                table: "Student",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "AspNetUsers",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "AspNetUsers",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "AspNetUsers",
                maxLength: 100,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Student_CetUserId",
                table: "Student",
                column: "CetUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Student_AspNetUsers_CetUserId",
                table: "Student",
                column: "CetUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Student_AspNetUsers_CetUserId",
                table: "Student");

            migrationBuilder.DropIndex(
                name: "IX_Student_CetUserId",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "CetUserId",
                table: "Student");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "City",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<string>(
                name: "City",
                table: "Student",
                nullable: false,
                defaultValue: "");
        }
    }
}
