using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketPlace.Infastructure.Migrations
{
    /// <inheritdoc />
    public partial class Ini2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Vaucers_ProductId",
                table: "Vaucers");

            migrationBuilder.CreateIndex(
                name: "IX_Vaucers_ProductId",
                table: "Vaucers",
                column: "ProductId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Vaucers_ProductId",
                table: "Vaucers");

            migrationBuilder.CreateIndex(
                name: "IX_Vaucers_ProductId",
                table: "Vaucers",
                column: "ProductId");
        }
    }
}
