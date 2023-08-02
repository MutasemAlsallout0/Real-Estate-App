using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aqar.Data.Migrations
{
    /// <inheritdoc />
    public partial class removeActiveStateandAddFollowersCountforpublicPage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActiveState",
                table: "PublicPages");

            migrationBuilder.AddColumn<int>(
                name: "FollowersCount",
                table: "PublicPages",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FollowersCount",
                table: "PublicPages");

            migrationBuilder.AddColumn<bool>(
                name: "ActiveState",
                table: "PublicPages",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
