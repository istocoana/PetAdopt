using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetAdopt.Migrations
{
    public partial class AddLocationToPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_Animal_animalID",
                table: "Post");

            migrationBuilder.AlterColumn<int>(
                name: "animalID",
                table: "Post",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Post",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_Animal_animalID",
                table: "Post",
                column: "animalID",
                principalTable: "Animal",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Post_Animal_animalID",
                table: "Post");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Post");

            migrationBuilder.AlterColumn<int>(
                name: "animalID",
                table: "Post",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Post_Animal_animalID",
                table: "Post",
                column: "animalID",
                principalTable: "Animal",
                principalColumn: "id");
        }
    }
}
