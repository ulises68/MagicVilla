using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace villaMagica.Migrations
{
    /// <inheritdoc />
    public partial class AlimentarTablaVilla : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Villas",
                columns: new[] { "Id", "Amenidad", "Detalle", "FechaActualizacion", "FechaCreacion", "ImagenUrl", "MetrosCuadrados", "Nombre", "Ocupantes", "Tarifa" },
                values: new object[,]
                {
                    { 1, "Sonido Inteligente", "Villa en el lago  ....", new DateTime(2023, 5, 23, 11, 46, 52, 846, DateTimeKind.Local).AddTicks(6582), new DateTime(2023, 5, 23, 11, 46, 52, 846, DateTimeKind.Local).AddTicks(6547), "", 50, "Villa Real", 5, 200.0 },
                    { 2, "Luz Inteligente", "Villa con puesta al mar....", new DateTime(2023, 5, 23, 11, 46, 52, 846, DateTimeKind.Local).AddTicks(6590), new DateTime(2023, 5, 23, 11, 46, 52, 846, DateTimeKind.Local).AddTicks(6588), "", 40, "Premiu Vista a la Piscina", 4, 150.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Villas",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
