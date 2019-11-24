using Microsoft.EntityFrameworkCore.Migrations;

namespace EcoHelper.Persistence.Migrations
{
    public partial class BaseVersion : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<double>(
                name: "Ver",
                table: "BaseVersions",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Ver",
                table: "BaseVersions",
                nullable: true,
                oldClrType: typeof(double));
        }
    }
}
