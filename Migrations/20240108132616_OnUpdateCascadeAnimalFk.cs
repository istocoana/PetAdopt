using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetAdopt.Migrations
{
    public partial class OnUpdateCascadeAnimalFk : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE [dbo].[Post] DROP CONSTRAINT [FK_Post_Animal_animalID]");

            migrationBuilder.Sql(@"
                ALTER TABLE [dbo].[Post]
                ADD CONSTRAINT [FK_Post_Animal_animalID] 
                FOREIGN KEY ([AnimalId]) 
                REFERENCES [dbo].[Animal] ([id]) 
                ON DELETE CASCADE 
                ON UPDATE CASCADE;
            ");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("ALTER TABLE [dbo].[Post] DROP CONSTRAINT [FK_Post_Animal_animalID]");
        }
    }
}
