using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace modelDB.Migrations
{
    /// <inheritdoc />
    public partial class InitialSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "makes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    country = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    founded_year = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_makes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "car_models",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    year = table.Column<int>(type: "int", nullable: true),
                    engine = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    transmission = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    body_style = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: true),
                    price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    make_id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_car_models", x => x.id);
                    table.ForeignKey(
                        name: "FK_car_models_makes_make_id",
                        column: x => x.make_id,
                        principalTable: "makes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_car_models_make_id",
                table: "car_models",
                column: "make_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "car_models");

            migrationBuilder.DropTable(
                name: "makes");
        }
    }
}
