using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace denizdikbiyik_CET322_HW5.Data.Migrations
{
    public partial class sonson : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CetUser2Id",
                table: "Department",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CetUserId2",
                table: "Department",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "Department",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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

        protected override void Down(MigrationBuilder migrationBuilder)
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
                name: "CetUserId2",
                table: "Department");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
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
        }
    }
}
