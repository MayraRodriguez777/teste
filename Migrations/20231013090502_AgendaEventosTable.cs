using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication2.Migrations
{
    public partial class AgendaEventosTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Telefone",
                table: "UsuarioCliente",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "AgendaEventos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataHora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Titulo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UsuarioClienteId = table.Column<int>(type: "int", nullable: false),
                    UsuarioDentistaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AgendaEventos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AgendaEventos_UsuarioCliente_UsuarioClienteId",
                        column: x => x.UsuarioClienteId,
                        principalTable: "UsuarioCliente",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AgendaEventos_UsuarioDentista_UsuarioDentistaId",
                        column: x => x.UsuarioDentistaId,
                        principalTable: "UsuarioDentista",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AgendaEventos_UsuarioClienteId",
                table: "AgendaEventos",
                column: "UsuarioClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_AgendaEventos_UsuarioDentistaId",
                table: "AgendaEventos",
                column: "UsuarioDentistaId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AgendaEventos");

            migrationBuilder.AlterColumn<int>(
                name: "Telefone",
                table: "UsuarioCliente",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
