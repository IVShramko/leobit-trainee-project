using Microsoft.EntityFrameworkCore.Migrations;

namespace Dating.Logic.Migrations
{
    public partial class AdditionalConstraints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfiles_AspNetUsers_AspNetUserId",
                table: "UserProfiles");

            migrationBuilder.DropIndex(
                name: "IX_UserProfiles_AspNetUserId",
                table: "UserProfiles");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "UserProfiles",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AspNetUserId",
                table: "UserProfiles",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450,
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_AspNetUserId",
                table: "UserProfiles",
                column: "AspNetUserId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfiles_AspNetUsers_AspNetUserId",
                table: "UserProfiles",
                column: "AspNetUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserProfiles_AspNetUsers_AspNetUserId",
                table: "UserProfiles");

            migrationBuilder.DropIndex(
                name: "IX_UserProfiles_AspNetUserId",
                table: "UserProfiles");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                table: "UserProfiles",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256);

            migrationBuilder.AlterColumn<string>(
                name: "AspNetUserId",
                table: "UserProfiles",
                type: "nvarchar(450)",
                maxLength: 450,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldMaxLength: 450);

            migrationBuilder.CreateIndex(
                name: "IX_UserProfiles_AspNetUserId",
                table: "UserProfiles",
                column: "AspNetUserId",
                unique: true,
                filter: "[AspNetUserId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_UserProfiles_AspNetUsers_AspNetUserId",
                table: "UserProfiles",
                column: "AspNetUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
