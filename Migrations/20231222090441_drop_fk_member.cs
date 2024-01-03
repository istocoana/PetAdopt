using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetAdopt.Migrations
{
    public partial class drop_fk_member : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
             name: "IX_Animal_MemberID",
             table: "Animal");

            migrationBuilder.DropIndex(
                name: "IX_Post_MemberID",
                table: "Post");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
               name: "IX_Animal_MemberID",
               table: "Animal",
               column: "MemberID");

            migrationBuilder.CreateIndex(
              name: "IX_Post_MemberID",
              table: "Post",
              column: "MemberID");
        }

    }
}
