using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dating.Logic.Migrations
{
    public partial class photo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPhotos_UserAlbums_UserAlbumId",
                table: "UserPhotos");

            migrationBuilder.DropIndex(
                name: "IX_UserPhotos_UserAlbumId",
                table: "UserPhotos");

            migrationBuilder.DropColumn(
                name: "UserAlbumId",
                table: "UserPhotos");

            migrationBuilder.CreateIndex(
                name: "IX_UserPhotos_AlbumId",
                table: "UserPhotos",
                column: "AlbumId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPhotos_UserAlbums_AlbumId",
                table: "UserPhotos",
                column: "AlbumId",
                principalTable: "UserAlbums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPhotos_UserAlbums_AlbumId",
                table: "UserPhotos");

            migrationBuilder.DropIndex(
                name: "IX_UserPhotos_AlbumId",
                table: "UserPhotos");

            migrationBuilder.AddColumn<Guid>(
                name: "UserAlbumId",
                table: "UserPhotos",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserPhotos_UserAlbumId",
                table: "UserPhotos",
                column: "UserAlbumId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPhotos_UserAlbums_UserAlbumId",
                table: "UserPhotos",
                column: "UserAlbumId",
                principalTable: "UserAlbums",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
