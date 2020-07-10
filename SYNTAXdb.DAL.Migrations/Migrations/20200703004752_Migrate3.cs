using Microsoft.EntityFrameworkCore.Migrations;

namespace SYNTAXdb.DAL.Migrations.Migrations
{
    public partial class Migrate3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DiscordName",
                table: "Profiles",
                nullable: true,
                oldClrType: typeof(decimal),
                oldType: "decimal(20,0)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "DiscordName",
                table: "Profiles",
                type: "decimal(20,0)",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
