using Microsoft.EntityFrameworkCore.Migrations;

namespace FTData.DbContext.LicenseDbContext.Migrations
{
    public partial class removingcolumncreatedby : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created_By_ID",
                table: "Estimates");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Created_By_ID",
                table: "Estimates",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
