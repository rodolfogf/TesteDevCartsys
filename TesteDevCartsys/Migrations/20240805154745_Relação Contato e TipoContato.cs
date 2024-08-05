using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TesteDevCartsys.Migrations
{
    public partial class RelaçãoContatoeTipoContato : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TipoContatoId",
                table: "Contatos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TiposContato",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Descricao = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposContato", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Contatos_TipoContatoId",
                table: "Contatos",
                column: "TipoContatoId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Contatos_TiposContato_TipoContatoId",
                table: "Contatos",
                column: "TipoContatoId",
                principalTable: "TiposContato",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Contatos_TiposContato_TipoContatoId",
                table: "Contatos");

            migrationBuilder.DropTable(
                name: "TiposContato");

            migrationBuilder.DropIndex(
                name: "IX_Contatos_TipoContatoId",
                table: "Contatos");

            migrationBuilder.DropColumn(
                name: "TipoContatoId",
                table: "Contatos");
        }
    }
}
