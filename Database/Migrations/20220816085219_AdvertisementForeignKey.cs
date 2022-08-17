using Microsoft.EntityFrameworkCore.Migrations;

namespace backendtemplate.Migrations
{
    public partial class AdvertisementForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Advertisements");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "FavoriteAdvertisement",
                newName: "UserEmail");

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                table: "Advertisements",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FavoriteAdvertisement_AdvertisementId",
                table: "FavoriteAdvertisement",
                column: "AdvertisementId");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteAdvertisement_Advertisements_AdvertisementId",
                table: "FavoriteAdvertisement",
                column: "AdvertisementId",
                principalTable: "Advertisements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteAdvertisement_Advertisements_AdvertisementId",
                table: "FavoriteAdvertisement");

            migrationBuilder.DropIndex(
                name: "IX_FavoriteAdvertisement_AdvertisementId",
                table: "FavoriteAdvertisement");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                table: "Advertisements");

            migrationBuilder.RenameColumn(
                name: "UserEmail",
                table: "FavoriteAdvertisement",
                newName: "UserId");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Advertisements",
                nullable: false,
                defaultValue: 0);
        }
    }
}
