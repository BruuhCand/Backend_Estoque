using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace api_estoque.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "DataValidade",
                table: "Validade",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT");

            migrationBuilder.AddColumn<int>(
                name: "ProdutoId1",
                table: "EstoqueProdutos",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_EstoqueProdutos_ProdutoId1",
                table: "EstoqueProdutos",
                column: "ProdutoId1",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_EstoqueProdutos_Produto_ProdutoId1",
                table: "EstoqueProdutos",
                column: "ProdutoId1",
                principalTable: "Produto",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EstoqueProdutos_Produto_ProdutoId1",
                table: "EstoqueProdutos");

            migrationBuilder.DropIndex(
                name: "IX_EstoqueProdutos_ProdutoId1",
                table: "EstoqueProdutos");

            migrationBuilder.DropColumn(
                name: "ProdutoId1",
                table: "EstoqueProdutos");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DataValidade",
                table: "Validade",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true);
        }
    }
}
