using Microsoft.EntityFrameworkCore.Migrations;

namespace WebVuiVN.Migrations
{
    public partial class _4 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUserChats_RoomChats_RoomChatId1",
                table: "AppUserChats");

            migrationBuilder.DropIndex(
                name: "IX_AppUserChats_RoomChatId1",
                table: "AppUserChats");

            migrationBuilder.DropColumn(
                name: "RoomChatId1",
                table: "AppUserChats");

            migrationBuilder.AlterColumn<int>(
                name: "RoomChatId",
                table: "AppUserChats",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppUserChats_RoomChatId",
                table: "AppUserChats",
                column: "RoomChatId");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserChats_RoomChats_RoomChatId",
                table: "AppUserChats",
                column: "RoomChatId",
                principalTable: "RoomChats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUserChats_RoomChats_RoomChatId",
                table: "AppUserChats");

            migrationBuilder.DropIndex(
                name: "IX_AppUserChats_RoomChatId",
                table: "AppUserChats");

            migrationBuilder.AlterColumn<string>(
                name: "RoomChatId",
                table: "AppUserChats",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "RoomChatId1",
                table: "AppUserChats",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppUserChats_RoomChatId1",
                table: "AppUserChats",
                column: "RoomChatId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUserChats_RoomChats_RoomChatId1",
                table: "AppUserChats",
                column: "RoomChatId1",
                principalTable: "RoomChats",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
