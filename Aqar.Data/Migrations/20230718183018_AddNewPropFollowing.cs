using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aqar.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddNewPropFollowing : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsFollow",
                table: "Followings",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsFollow",
                table: "Followings");
        }
    }
}
