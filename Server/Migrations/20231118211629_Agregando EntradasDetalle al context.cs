using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace _2doParcial_AP1RonellIntento.Server.Migrations
{
    /// <inheritdoc />
    public partial class AgregandoEntradasDetallealcontext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EntradasDetalle_Entradas_EntradaId",
                table: "EntradasDetalle");

            migrationBuilder.DropForeignKey(
                name: "FK_EntradasDetalle_Productos_ProductoId",
                table: "EntradasDetalle");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EntradasDetalle",
                table: "EntradasDetalle");

            migrationBuilder.RenameTable(
                name: "EntradasDetalle",
                newName: "EntradasDetalles");

            migrationBuilder.RenameIndex(
                name: "IX_EntradasDetalle_ProductoId",
                table: "EntradasDetalles",
                newName: "IX_EntradasDetalles_ProductoId");

            migrationBuilder.RenameIndex(
                name: "IX_EntradasDetalle_EntradaId",
                table: "EntradasDetalles",
                newName: "IX_EntradasDetalles_EntradaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EntradasDetalles",
                table: "EntradasDetalles",
                column: "DetalleId");

            migrationBuilder.AddForeignKey(
                name: "FK_EntradasDetalles_Entradas_EntradaId",
                table: "EntradasDetalles",
                column: "EntradaId",
                principalTable: "Entradas",
                principalColumn: "EntradaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EntradasDetalles_Productos_ProductoId",
                table: "EntradasDetalles",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "ProductoId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_EntradasDetalles_Entradas_EntradaId",
                table: "EntradasDetalles");

            migrationBuilder.DropForeignKey(
                name: "FK_EntradasDetalles_Productos_ProductoId",
                table: "EntradasDetalles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_EntradasDetalles",
                table: "EntradasDetalles");

            migrationBuilder.RenameTable(
                name: "EntradasDetalles",
                newName: "EntradasDetalle");

            migrationBuilder.RenameIndex(
                name: "IX_EntradasDetalles_ProductoId",
                table: "EntradasDetalle",
                newName: "IX_EntradasDetalle_ProductoId");

            migrationBuilder.RenameIndex(
                name: "IX_EntradasDetalles_EntradaId",
                table: "EntradasDetalle",
                newName: "IX_EntradasDetalle_EntradaId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_EntradasDetalle",
                table: "EntradasDetalle",
                column: "DetalleId");

            migrationBuilder.AddForeignKey(
                name: "FK_EntradasDetalle_Entradas_EntradaId",
                table: "EntradasDetalle",
                column: "EntradaId",
                principalTable: "Entradas",
                principalColumn: "EntradaId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EntradasDetalle_Productos_ProductoId",
                table: "EntradasDetalle",
                column: "ProductoId",
                principalTable: "Productos",
                principalColumn: "ProductoId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
