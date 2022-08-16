using Microsoft.EntityFrameworkCore.Migrations;

namespace backendtemplate.Migrations
{
    public partial class AddUserEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavouriteAdvertisement_Advertisements_AdvertisementId",
                table: "FavouriteAdvertisement");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavouriteAdvertisement",
                table: "FavouriteAdvertisement");

            migrationBuilder.RenameTable(
                name: "FavouriteAdvertisement",
                newName: "FavouriteAdvertisements");

            migrationBuilder.RenameIndex(
                name: "IX_FavouriteAdvertisement_AdvertisementId",
                table: "FavouriteAdvertisements",
                newName: "IX_FavouriteAdvertisements_AdvertisementId");

            migrationBuilder.AlterColumn<string>(
                name: "UserEmail",
                table: "FavouriteAdvertisements",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavouriteAdvertisements",
                table: "FavouriteAdvertisements",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FavouriteAdvertisements_Advertisements_AdvertisementId",
                table: "FavouriteAdvertisements",
                column: "AdvertisementId",
                principalTable: "Advertisements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavouriteAdvertisements_Advertisements_AdvertisementId",
                table: "FavouriteAdvertisements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavouriteAdvertisements",
                table: "FavouriteAdvertisements");

            migrationBuilder.RenameTable(
                name: "FavouriteAdvertisements",
                newName: "FavouriteAdvertisement");

            migrationBuilder.RenameIndex(
                name: "IX_FavouriteAdvertisements_AdvertisementId",
                table: "FavouriteAdvertisement",
                newName: "IX_FavouriteAdvertisement_AdvertisementId");

            migrationBuilder.AlterColumn<int>(
                name: "UserEmail",
                table: "FavouriteAdvertisement",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavouriteAdvertisement",
                table: "FavouriteAdvertisement",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FavouriteAdvertisement_Advertisements_AdvertisementId",
                table: "FavouriteAdvertisement",
                column: "AdvertisementId",
                principalTable: "Advertisements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
