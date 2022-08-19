using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace backendtemplate.Migrations
{
    public partial class FailedToSyncFlag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "FailedToSync",
                table: "Advertisements",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FailedToSync",
                table: "Advertisements");
        }
    }
}
