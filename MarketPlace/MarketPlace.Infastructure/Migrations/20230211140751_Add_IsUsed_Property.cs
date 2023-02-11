using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketPlace.Infastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIsUsedProperty : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsUsed",
                table: "Vaucers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsUsed",
                table: "Vaucers");
        }
    }
}
