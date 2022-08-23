using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Dating.Logic.Migrations
{
    public partial class ChatMemberChatLookUpTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ChatMembers_Chats_ChatId",
                table: "ChatMembers");

            migrationBuilder.DropIndex(
                name: "IX_ChatMembers_ChatId",
                table: "ChatMembers");

            migrationBuilder.CreateTable(
                name: "ChatMemberChats",
                columns: table => new
                {
                    ChatId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ChatMemberId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChatMemberChats", x => new { x.ChatId, x.ChatMemberId });
                    table.ForeignKey(
                        name: "FK_ChatMemberChats_ChatMembers_ChatMemberId",
                        column: x => x.ChatMemberId,
                        principalTable: "ChatMembers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ChatMemberChats_Chats_ChatId",
                        column: x => x.ChatId,
                        principalTable: "Chats",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ChatMemberChats_ChatMemberId",
                table: "ChatMemberChats",
                column: "ChatMemberId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChatMemberChats");

            migrationBuilder.CreateIndex(
                name: "IX_ChatMembers_ChatId",
                table: "ChatMembers",
                column: "ChatId");

            migrationBuilder.AddForeignKey(
                name: "FK_ChatMembers_Chats_ChatId",
                table: "ChatMembers",
                column: "ChatId",
                principalTable: "Chats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
