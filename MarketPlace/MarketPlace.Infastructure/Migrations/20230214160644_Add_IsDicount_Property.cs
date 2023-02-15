using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketPlace.Infastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIsDicountProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDiscount",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDiscount",
                table: "Products");
        }
    }
}
