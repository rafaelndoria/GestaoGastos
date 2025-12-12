using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GestaoGastos.Infra.Migrations
{
    /// <inheritdoc />
    public partial class RemovidoColunaTipoCategoriaDaTabelaCategorias : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: new Guid("db611e19-521c-4994-942c-6e5db686d86e"));

            migrationBuilder.DropColumn(
                name: "TipoCategoria",
                table: "Categoria");

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Ativo", "CriadoEm", "DataCriacao", "Email", "Nome", "Role", "SenhaHash" },
                values: new object[] { new Guid("a8dd9481-85bf-4c3a-a770-47aff17774dd"), true, new DateTime(2025, 12, 12, 17, 7, 38, 270, DateTimeKind.Utc).AddTicks(3723), new DateTime(2025, 12, 12, 17, 7, 38, 270, DateTimeKind.Utc).AddTicks(4284), "admin@gmail.com", "admin", 0, "b8b8eb83374c0bf3b1c3224159f6119dbfff1b7ed6dfecdd80d4e8a895790a34" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Usuarios",
                keyColumn: "Id",
                keyValue: new Guid("a8dd9481-85bf-4c3a-a770-47aff17774dd"));

            migrationBuilder.AddColumn<int>(
                name: "TipoCategoria",
                table: "Categoria",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Ativo", "CriadoEm", "DataCriacao", "Email", "Nome", "Role", "SenhaHash" },
                values: new object[] { new Guid("db611e19-521c-4994-942c-6e5db686d86e"), true, new DateTime(2025, 12, 10, 17, 50, 55, 88, DateTimeKind.Utc).AddTicks(7743), new DateTime(2025, 12, 10, 17, 50, 55, 88, DateTimeKind.Utc).AddTicks(8264), "admin@gmail.com", "admin", 0, "b8b8eb83374c0bf3b1c3224159f6119dbfff1b7ed6dfecdd80d4e8a895790a34" });
        }
    }
}
