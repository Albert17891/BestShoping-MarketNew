using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MarketPlace.Infastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserProductCard : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Counter",
                table: "Products");

            migrationBuilder.CreateTable(
                name: "UserProductCard",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProductCard", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserProductCard_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserProductCard_UserId",
                table: "UserProductCard",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserProductCard");

            migrationBuilder.AddColumn<int>(
                name: "Counter",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
