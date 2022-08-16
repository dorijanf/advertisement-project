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
                table: "FavouriteAdvertisement",
                newName: "UserEmail");

            migrationBuilder.AddColumn<string>(
                name: "UserEmail",
                table: "Advertisements",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_FavouriteAdvertisement_AdvertisementId",
                table: "FavouriteAdvertisement",
                column: "AdvertisementId");

            migrationBuilder.AddForeignKey(
                name: "FK_FavouriteAdvertisement_Advertisements_AdvertisementId",
                table: "FavouriteAdvertisement",
                column: "AdvertisementId",
                principalTable: "Advertisements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavouriteAdvertisement_Advertisements_AdvertisementId",
                table: "FavouriteAdvertisement");

            migrationBuilder.DropIndex(
                name: "IX_FavouriteAdvertisement_AdvertisementId",
                table: "FavouriteAdvertisement");

            migrationBuilder.DropColumn(
                name: "UserEmail",
                table: "Advertisements");

            migrationBuilder.RenameColumn(
                name: "UserEmail",
                table: "FavouriteAdvertisement",
                newName: "UserId");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Advertisements",
                nullable: false,
                defaultValue: 0);
        }
    }
}
