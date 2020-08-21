using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.Data.EntityFrameworkCore.Metadata;

namespace TiendaServicios.Api.CarritoCompra.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CarritoSesion",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    FechaCreacion = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarritoSesion", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CarrioSesionDetalle",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    FechaCreacion = table.Column<DateTime>(nullable: true),
                    ProductoSeleccionado = table.Column<string>(nullable: true),
                    CarritoSesionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarrioSesionDetalle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CarrioSesionDetalle_CarritoSesion_CarritoSesionId",
                        column: x => x.CarritoSesionId,
                        principalTable: "CarritoSesion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CarrioSesionDetalle_CarritoSesionId",
                table: "CarrioSesionDetalle",
                column: "CarritoSesionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CarrioSesionDetalle");

            migrationBuilder.DropTable(
                name: "CarritoSesion");
        }
    }
}
