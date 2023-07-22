using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Aqar.Data.Migrations
{
    /// <inheritdoc />
    public partial class appforinkeyestateattachment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachments_Estates_EstateId",
                table: "Attachments");

            migrationBuilder.AlterColumn<int>(
                name: "EstateId",
                table: "Attachments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Attachments_Estates_EstateId",
                table: "Attachments",
                column: "EstateId",
                principalTable: "Estates",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachments_Estates_EstateId",
                table: "Attachments");

            migrationBuilder.AlterColumn<int>(
                name: "EstateId",
                table: "Attachments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachments_Estates_EstateId",
                table: "Attachments",
                column: "EstateId",
                principalTable: "Estates",
                principalColumn: "Id");
        }
    }
}
