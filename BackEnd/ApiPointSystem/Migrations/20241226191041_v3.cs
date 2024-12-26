using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PointSystem.Migrations
{
    public partial class v3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DataRegistro",
                table: "Pontos",
                newName: "Hora");

            migrationBuilder.AddColumn<DateOnly>(
                name: "Data",
                table: "Pontos",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Data",
                table: "Pontos");

            migrationBuilder.RenameColumn(
                name: "Hora",
                table: "Pontos",
                newName: "DataRegistro");
        }
    }
}
