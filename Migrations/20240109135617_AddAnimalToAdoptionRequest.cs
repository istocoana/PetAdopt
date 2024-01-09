using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetAdopt.Migrations
{
    public partial class AddAnimalToAdoptionRequest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
         
            migrationBuilder.CreateIndex(
                name: "IX_AdoptionRequest_AnimalId",
                table: "AdoptionRequest",
                column: "AnimalId");

            migrationBuilder.AddForeignKey(
                name: "FK_AdoptionRequest_Animal_AnimalId",
                table: "AdoptionRequest",
                column: "AnimalId",
                principalTable: "Animal",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict,
                onUpdate: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AdoptionRequest_Animal_AnimalId",
                table: "AdoptionRequest");

            migrationBuilder.DropIndex(
                name: "IX_AdoptionRequest_AnimalId",
                table: "AdoptionRequest");

            migrationBuilder.DropColumn(
                name: "AnimalId",
                table: "AdoptionRequest");
        }
    }
}
