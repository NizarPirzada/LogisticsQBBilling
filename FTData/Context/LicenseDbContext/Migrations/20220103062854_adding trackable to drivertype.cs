using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FTData.DbContext.LicenseDbContext.Migrations
{
    public partial class addingtrackabletodrivertype : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estimates_AspNetUsers_LicenseUserId",
                table: "Estimates");

            migrationBuilder.AlterColumn<long>(
                name: "LicenseUserId",
                table: "Estimates",
                type: "bigint",
                nullable: false,
                defaultValue: 0L,
                oldClrType: typeof(long),
                oldType: "bigint",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "DriverTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "DriverTypes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedBy",
                table: "DriverTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedDate",
                table: "DriverTypes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "DriverTypes",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "UpdatedBy",
                table: "DriverTypes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedDate",
                table: "DriverTypes",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Estimates_AspNetUsers_LicenseUserId",
                table: "Estimates",
                column: "LicenseUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Estimates_AspNetUsers_LicenseUserId",
                table: "Estimates");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "DriverTypes");

            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "DriverTypes");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "DriverTypes");

            migrationBuilder.DropColumn(
                name: "DeletedDate",
                table: "DriverTypes");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "DriverTypes");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "DriverTypes");

            migrationBuilder.DropColumn(
                name: "UpdatedDate",
                table: "DriverTypes");

            migrationBuilder.AlterColumn<long>(
                name: "LicenseUserId",
                table: "Estimates",
                type: "bigint",
                nullable: true,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddForeignKey(
                name: "FK_Estimates_AspNetUsers_LicenseUserId",
                table: "Estimates",
                column: "LicenseUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
