using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SomaShare.Migrations
{
    /// <inheritdoc />
    public partial class ConfigureFavouritesAndPrecision : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favourites_Textbooks_TextbookId",
                table: "Favourites");
            
            //migrationBuilder.DropIndex(
            //    name: "IX_Favourites_UserId",
            //    table: "Favourites");
            
            migrationBuilder.CreateIndex(
                name: "IX_Favourites_UserId_TextbookId",
                table: "Favourites",
                columns: new[] { "UserId", "TextbookId" },
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Favourites_Textbooks_TextbookId",
                table: "Favourites",
                column: "TextbookId",
                principalTable: "Textbooks",
                principalColumn: "TextbookId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Favourites_Textbooks_TextbookId",
                table: "Favourites");
            
            // migrationBuilder.DropIndex(
            //    name: "IX_Favourites_UserId_TextbookId",
            //    table: "Favourites");

            migrationBuilder.CreateIndex(
                name: "IX_Favourites_UserId",
                table: "Favourites",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Favourites_Textbooks_TextbookId",
                table: "Favourites",
                column: "TextbookId",
                principalTable: "Textbooks",
                principalColumn: "TextbookId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
