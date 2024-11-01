using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AgroClimate.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FazendasSP",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Area = table.Column<int>(type: "NUMBER(10)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FazendasSP", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NomeDaTabelaRealNoBanco",
                columns: table => new
                {
                    Id = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    Nome = table.Column<string>(type: "NVARCHAR2(100)", maxLength: 100, nullable: false),
                    Cpf = table.Column<string>(type: "NVARCHAR2(11)", maxLength: 11, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NomeDaTabelaRealNoBanco", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AgricultorFazenda",
                columns: table => new
                {
                    AgricultorId = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    FazendaId = table.Column<int>(type: "NUMBER(10)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgricultorFazenda", x => new { x.AgricultorId, x.FazendaId });
                    table.ForeignKey(
                        name: "FK_AgricultorFazenda_FazendasSP_FazendaId",
                        column: x => x.FazendaId,
                        principalTable: "FazendasSP",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AgricultorFazenda_NomeDaTabelaRealNoBanco_AgricultorId",
                        column: x => x.AgricultorId,
                        principalTable: "NomeDaTabelaRealNoBanco",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AgricultorFazenda_FazendaId",
                table: "AgricultorFazenda",
                column: "FazendaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AgricultorFazenda");

            migrationBuilder.DropTable(
                name: "FazendasSP");

            migrationBuilder.DropTable(
                name: "NomeDaTabelaRealNoBanco");
        }
    }
}
