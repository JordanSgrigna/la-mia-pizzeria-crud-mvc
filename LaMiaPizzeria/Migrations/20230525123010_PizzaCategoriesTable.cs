using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LaMiaPizzeria.Migrations
{
    /// <inheritdoc />
    public partial class PizzaCategoriesTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CategoryId",
                table: "Pizza",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PizzaCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PizzaCategories", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Pizza_CategoryId",
                table: "Pizza",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pizza_PizzaCategories_CategoryId",
                table: "Pizza",
                column: "CategoryId",
                principalTable: "PizzaCategories",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pizza_PizzaCategories_CategoryId",
                table: "Pizza");

            migrationBuilder.DropTable(
                name: "PizzaCategories");

            migrationBuilder.DropIndex(
                name: "IX_Pizza_CategoryId",
                table: "Pizza");

            migrationBuilder.DropColumn(
                name: "CategoryId",
                table: "Pizza");
        }
    }
}
