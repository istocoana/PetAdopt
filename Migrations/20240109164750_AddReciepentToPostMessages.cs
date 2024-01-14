using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetAdopt.Migrations
{
    public partial class AddReciepentToPostMessages : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<string>(
             name: "RecipientId",
             table: "PostMessages",
             type: "nvarchar(450)",
             nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_PostMessages_RecipientId",
                table: "PostMessages",
                column: "RecipientId");

            migrationBuilder.AddForeignKey(
                name: "FK_PostMessages_AspNetUsers_RecipientId",
                table: "PostMessages",
                column: "RecipientId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostMessages_AspNetUsers_RecipientId",
                table: "PostMessages");

            migrationBuilder.DropIndex(
                name: "IX_PostMessages_RecipientId",
                table: "PostMessages");

            migrationBuilder.DropColumn(
                name: "RecipientId",
                table: "PostMessages");
        }

    }
}
