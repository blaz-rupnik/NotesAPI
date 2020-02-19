using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NotesApp.Migrations
{
    public partial class NullableFolder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Folders_FolderId",
                table: "Notes");

            migrationBuilder.AlterColumn<Guid>(
                name: "FolderId",
                table: "Notes",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Folders_FolderId",
                table: "Notes",
                column: "FolderId",
                principalTable: "Folders",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notes_Folders_FolderId",
                table: "Notes");

            migrationBuilder.AlterColumn<Guid>(
                name: "FolderId",
                table: "Notes",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Notes_Folders_FolderId",
                table: "Notes",
                column: "FolderId",
                principalTable: "Folders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
