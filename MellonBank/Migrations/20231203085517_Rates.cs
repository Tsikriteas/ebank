using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MellonBank.Migrations
{
    /// <inheritdoc />
    public partial class Rates : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Rates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AUD = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CHF = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    GBP = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    USD = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rates", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rates");
        }
    }
}
