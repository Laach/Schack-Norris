using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Norris.Data.Migrations
{
    public partial class InitDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Friends",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    FriendID = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friends", x => new { x.UserId, x.FriendID });
                    table.ForeignKey(
                        name: "FK_Friends_AspNetUsers_FriendID",
                        column: x => x.FriendID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Friends_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GameSessions",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Board = table.Column<string>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsWhitePlayerTurn = table.Column<bool>(nullable: false),
                    Log = table.Column<string>(nullable: false),
                    PlayerBlackID = table.Column<string>(nullable: false),
                    PlayerWhiteID = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GameSessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GameSessions_AspNetUsers_PlayerBlackID",
                        column: x => x.PlayerBlackID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_GameSessions_AspNetUsers_PlayerWhiteID",
                        column: x => x.PlayerWhiteID,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Friends_FriendID",
                table: "Friends",
                column: "FriendID");

            migrationBuilder.CreateIndex(
                name: "IX_GameSessions_PlayerBlackID",
                table: "GameSessions",
                column: "PlayerBlackID");

            migrationBuilder.CreateIndex(
                name: "IX_GameSessions_PlayerWhiteID",
                table: "GameSessions",
                column: "PlayerWhiteID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Friends");

            migrationBuilder.DropTable(
                name: "GameSessions");
        }
    }
}
