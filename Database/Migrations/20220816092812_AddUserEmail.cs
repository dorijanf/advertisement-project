using Microsoft.EntityFrameworkCore.Migrations;

namespace backendtemplate.Migrations
{
    public partial class AddUserEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteAdvertisement_Advertisements_AdvertisementId",
                table: "FavoriteAdvertisement");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavoriteAdvertisement",
                table: "FavoriteAdvertisement");

            migrationBuilder.RenameTable(
                name: "FavoriteAdvertisement",
                newName: "FavoriteAdvertisements");

            migrationBuilder.RenameIndex(
                name: "IX_FavoriteAdvertisement_AdvertisementId",
                table: "FavoriteAdvertisements",
                newName: "IX_FavoriteAdvertisements_AdvertisementId");

            migrationBuilder.AlterColumn<string>(
                name: "UserEmail",
                table: "FavoriteAdvertisements",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavoriteAdvertisements",
                table: "FavoriteAdvertisements",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteAdvertisements_Advertisements_AdvertisementId",
                table: "FavoriteAdvertisements",
                column: "AdvertisementId",
                principalTable: "Advertisements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FavoriteAdvertisements_Advertisements_AdvertisementId",
                table: "FavoriteAdvertisements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FavoriteAdvertisements",
                table: "FavoriteAdvertisements");

            migrationBuilder.RenameTable(
                name: "FavoriteAdvertisements",
                newName: "FavoriteAdvertisement");

            migrationBuilder.RenameIndex(
                name: "IX_FavoriteAdvertisements_AdvertisementId",
                table: "FavoriteAdvertisement",
                newName: "IX_FavoriteAdvertisement_AdvertisementId");

            migrationBuilder.AlterColumn<int>(
                name: "UserEmail",
                table: "FavoriteAdvertisement",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddPrimaryKey(
                name: "PK_FavoriteAdvertisement",
                table: "FavoriteAdvertisement",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FavoriteAdvertisement_Advertisements_AdvertisementId",
                table: "FavoriteAdvertisement",
                column: "AdvertisementId",
                principalTable: "Advertisements",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
