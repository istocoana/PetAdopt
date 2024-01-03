using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetAdopt.Migrations
{
    public partial class AddIsAdoptedColumnToPost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAdopted",
                table: "Post",
                nullable: false,
                defaultValue: false);
        }
    }
}