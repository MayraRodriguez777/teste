using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication2.Migrations
{
    public partial class AddUsuarioDentista : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FotoContentType",
                table: "UsuarioDentista");

            migrationBuilder.DropColumn(
                name: "FotoFileName",
                table: "UsuarioDentista");

            migrationBuilder.AddColumn<string>(
                name: "Especialidade",
                table: "UsuarioDentista",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Especialidade",
                table: "UsuarioDentista");

            migrationBuilder.AddColumn<string>(
                name: "FotoContentType",
                table: "UsuarioDentista",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FotoFileName",
                table: "UsuarioDentista",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
