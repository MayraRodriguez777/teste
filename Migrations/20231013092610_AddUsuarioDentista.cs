using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication2.Migrations
{
    public partial class AddUsuarioDentista : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Foto",
                table: "UsuarioDentista",
                type: "varbinary(max)",
                nullable: true);

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Foto",
                table: "UsuarioDentista");

            migrationBuilder.DropColumn(
                name: "FotoContentType",
                table: "UsuarioDentista");

            migrationBuilder.DropColumn(
                name: "FotoFileName",
                table: "UsuarioDentista");
        }
    }
}
