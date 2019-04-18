using Microsoft.EntityFrameworkCore.Migrations;

namespace denizdikbiyik_CET322_HW5.Data.Migrations
{
    public partial class son1104 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Department_AspNetUsers_CetUser2Id",
                table: "Department");

            migrationBuilder.DropIndex(
                name: "IX_Department_CetUser2Id",
                table: "Department");

            migrationBuilder.DropColumn(
                name: "CetUser2Id",
                table: "Department");

            migrationBuilder.DropColumn(
                name: "CetUser2_City",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CetUser2_FirstName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CetUser2_LastName",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CetUser2_SchoolNo",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "CetUserId2",
                table: "Department",
                newName: "CetUserId");

            migrationBuilder.AlterColumn<string>(
                name: "CetUserId",
                table: "Department",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Department_CetUserId",
                table: "Department",
                column: "CetUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Department_AspNetUsers_CetUserId",
                table: "Department",
                column: "CetUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Department_AspNetUsers_CetUserId",
                table: "Department");

            migrationBuilder.DropIndex(
                name: "IX_Department_CetUserId",
                table: "Department");

            migrationBuilder.RenameColumn(
                name: "CetUserId",
                table: "Department",
                newName: "CetUserId2");

            migrationBuilder.AlterColumn<string>(
                name: "CetUserId2",
                table: "Department",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CetUser2Id",
                table: "Department",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CetUser2_City",
                table: "AspNetUsers",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CetUser2_FirstName",
                table: "AspNetUsers",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CetUser2_LastName",
                table: "AspNetUsers",
                maxLength: 100,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CetUser2_SchoolNo",
                table: "AspNetUsers",
                maxLength: 10,
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Department_CetUser2Id",
                table: "Department",
                column: "CetUser2Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Department_AspNetUsers_CetUser2Id",
                table: "Department",
                column: "CetUser2Id",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
