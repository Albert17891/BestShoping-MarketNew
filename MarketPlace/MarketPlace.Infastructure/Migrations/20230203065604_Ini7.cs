using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketPlace.Infastructure.Migrations
{
    /// <inheritdoc />
    public partial class Ini7 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserProductCard_ProductId",
                table: "UserProductCard");

            migrationBuilder.CreateIndex(
                name: "IX_UserProductCard_ProductId",
                table: "UserProductCard",
                column: "ProductId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserProductCard_ProductId",
                table: "UserProductCard");

            migrationBuilder.CreateIndex(
                name: "IX_UserProductCard_ProductId",
                table: "UserProductCard",
                column: "ProductId",
                unique: true);
        }
    }
}
