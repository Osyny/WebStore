using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WebStore.Migrations
{
    public partial class FixBasket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Basckets_Userss_UserId",
                table: "Basckets");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Basckets",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Basckets_Userss_UserId",
                table: "Basckets",
                column: "UserId",
                principalTable: "Userss",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Basckets_Userss_UserId",
                table: "Basckets");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Basckets",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Basckets_Userss_UserId",
                table: "Basckets",
                column: "UserId",
                principalTable: "Userss",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
