using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Catalog.Migrations
{
    /// <inheritdoc />
    public partial class foodCuisineRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CuisineId",
                table: "Foods",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Foods_CuisineId",
                table: "Foods",
                column: "CuisineId");

            migrationBuilder.AddForeignKey(
                name: "FK_Foods_Cuisines_CuisineId",
                table: "Foods",
                column: "CuisineId",
                principalTable: "Cuisines",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Foods_Cuisines_CuisineId",
                table: "Foods");

            migrationBuilder.DropIndex(
                name: "IX_Foods_CuisineId",
                table: "Foods");

            migrationBuilder.DropColumn(
                name: "CuisineId",
                table: "Foods");
        }
    }
}
