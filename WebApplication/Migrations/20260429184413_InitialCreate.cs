using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApplication.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Funcionarios",
                table: "Funcionarios");

            migrationBuilder.RenameTable(
                name: "Funcionarios",
                newName: "funcionarios");

            migrationBuilder.RenameColumn(
                name: "Turno",
                table: "funcionarios",
                newName: "turno");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "funcionarios",
                newName: "status");

            migrationBuilder.RenameColumn(
                name: "Sobrenome",
                table: "funcionarios",
                newName: "sobrenome");

            migrationBuilder.RenameColumn(
                name: "Nome",
                table: "funcionarios",
                newName: "nome");

            migrationBuilder.RenameColumn(
                name: "Departamento",
                table: "funcionarios",
                newName: "departamento");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "funcionarios",
                newName: "id");

            migrationBuilder.RenameColumn(
                name: "DataCriacao",
                table: "funcionarios",
                newName: "data_criacao");

            migrationBuilder.RenameColumn(
                name: "DataAlteracao",
                table: "funcionarios",
                newName: "data_alteracao");

            migrationBuilder.AlterColumn<bool>(
                name: "status",
                table: "funcionarios",
                type: "boolean",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "boolean");

            migrationBuilder.AlterColumn<string>(
                name: "sobrenome",
                table: "funcionarios",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "nome",
                table: "funcionarios",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<DateTime>(
                name: "data_criacao",
                table: "funcionarios",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<DateTime>(
                name: "data_alteracao",
                table: "funcionarios",
                type: "timestamp with time zone",
                nullable: false,
                defaultValueSql: "CURRENT_TIMESTAMP",
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");

            migrationBuilder.AddPrimaryKey(
                name: "PK_funcionarios",
                table: "funcionarios",
                column: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_funcionarios",
                table: "funcionarios");

            migrationBuilder.RenameTable(
                name: "funcionarios",
                newName: "Funcionarios");

            migrationBuilder.RenameColumn(
                name: "turno",
                table: "Funcionarios",
                newName: "Turno");

            migrationBuilder.RenameColumn(
                name: "status",
                table: "Funcionarios",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "sobrenome",
                table: "Funcionarios",
                newName: "Sobrenome");

            migrationBuilder.RenameColumn(
                name: "nome",
                table: "Funcionarios",
                newName: "Nome");

            migrationBuilder.RenameColumn(
                name: "departamento",
                table: "Funcionarios",
                newName: "Departamento");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Funcionarios",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "data_criacao",
                table: "Funcionarios",
                newName: "DataCriacao");

            migrationBuilder.RenameColumn(
                name: "data_alteracao",
                table: "Funcionarios",
                newName: "DataAlteracao");

            migrationBuilder.AlterColumn<bool>(
                name: "Status",
                table: "Funcionarios",
                type: "boolean",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<string>(
                name: "Sobrenome",
                table: "Funcionarios",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Nome",
                table: "Funcionarios",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataCriacao",
                table: "Funcionarios",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataAlteracao",
                table: "Funcionarios",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldDefaultValueSql: "CURRENT_TIMESTAMP");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Funcionarios",
                table: "Funcionarios",
                column: "Id");
        }
    }
}
