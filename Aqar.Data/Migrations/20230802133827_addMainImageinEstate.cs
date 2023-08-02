using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aqar.Data.Migrations
{
    /// <inheritdoc />
    public partial class addMainImageinEstate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MainImage",
                table: "Estates",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MainImage",
                table: "Estates");
        }
    }
}
