using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetAdopt.Migrations
{
    public partial class AddUserIdToAdoptionRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "AdoptionRequest",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_AdoptionRequest_UserId",
                table: "AdoptionRequest",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdoptionRequest_AspNetUsers_UserId",
                table: "AdoptionRequest",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction); 
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdoptionRequest_AspNetUsers_UserId",
                table: "AdoptionRequest");

            migrationBuilder.DropIndex(
                name: "IX_AdoptionRequest_UserId",
                table: "AdoptionRequest");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "AdoptionRequest",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
