using Microsoft.EntityFrameworkCore.Migrations;

namespace PetAdopt.Migrations
{
    public partial class AddPostMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PostMessages",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SenderId = table.Column<string>(nullable: true),
                    MessageContent = table.Column<string>(nullable: true),
                    DateSent = table.Column<DateTime>(nullable: false),
                    PostId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PostMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PostMessages_Posts_PostId",
                        column: x => x.PostId,
                        principalTable: "Post",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PostMessages");
        }
    }
}
