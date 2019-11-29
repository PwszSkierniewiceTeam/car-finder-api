using Microsoft.EntityFrameworkCore.Migrations;

namespace CarFinderAPI.Migrations
{
    public partial class CreteCarTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    BodyType = table.Column<string>(maxLength: 32, nullable: true),
                    Capacity = table.Column<double>(nullable: true),
                    Company = table.Column<string>(maxLength: 32, nullable: true),
                    DriveType = table.Column<string>(maxLength: 32, nullable: true),
                    Fuel = table.Column<string>(maxLength: 32, nullable: true),
                    FuelConsumption = table.Column<double>(nullable: true),
                    ImageUrl = table.Column<string>(nullable: true),
                    Model = table.Column<string>(maxLength: 32, nullable: true),
                    Power = table.Column<int>(nullable: true),
                    Price = table.Column<int>(nullable: true),
                    Transmission = table.Column<string>(maxLength: 32, nullable: true),
                    Version = table.Column<string>(maxLength: 32, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cars");
        }
    }
}