using Microsoft.EntityFrameworkCore.Migrations;

namespace WLoveAnimals.Services.Migrations
{
    public partial class spGetAnimalById : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string procedure = @"Create procedure spGetAnimalById
                        @Id int
                         as
                         Begin
                           Select * from Animals
	                     where Id = @Id
	                       End";
            migrationBuilder.Sql(procedure);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string procedure = @"Drop procedure spGetAnimalById";
            migrationBuilder.Sql(procedure);
        }
    }
}
