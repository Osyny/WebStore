using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebStore.Migrations
{
    public partial class FixUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Userss_AspNetUsers_AccountUserId",
                table: "Userss");

            migrationBuilder.DropIndex(
                name: "IX_Userss_AccountUserId",
                table: "Userss");

            migrationBuilder.AlterColumn<Guid>(
                name: "AccountUserId",
                table: "Userss",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AccountUserId1",
                table: "Userss",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Userss_AccountUserId1",
                table: "Userss",
                column: "AccountUserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Userss_AspNetUsers_AccountUserId1",
                table: "Userss",
                column: "AccountUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Userss_AspNetUsers_AccountUserId1",
                table: "Userss");

            migrationBuilder.DropIndex(
                name: "IX_Userss_AccountUserId1",
                table: "Userss");

            migrationBuilder.DropColumn(
                name: "AccountUserId1",
                table: "Userss");

            migrationBuilder.AlterColumn<string>(
                name: "AccountUserId",
                table: "Userss",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.CreateIndex(
                name: "IX_Userss_AccountUserId",
                table: "Userss",
                column: "AccountUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Userss_AspNetUsers_AccountUserId",
                table: "Userss",
                column: "AccountUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
