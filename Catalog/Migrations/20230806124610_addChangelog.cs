using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Catalog.Migrations
{
    /// <inheritdoc />
    public partial class addChangelog : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Changelogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Method = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TableName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    KeyValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NewValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OldValues = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IPAddress = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Changelogs", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Changelogs");
        }
    }
}
