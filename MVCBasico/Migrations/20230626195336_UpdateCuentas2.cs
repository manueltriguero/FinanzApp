using Microsoft.EntityFrameworkCore.Migrations;

namespace MVCBasico.Migrations
{
    public partial class UpdateCuentas2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cuentas_Usuarios_UsuarioId1",
                table: "Cuentas");

            migrationBuilder.DropIndex(
                name: "IX_Cuentas_UsuarioId1",
                table: "Cuentas");


            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "Cuentas",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateIndex(
                name: "IX_Cuentas_UsuarioId",
                table: "Cuentas",
                column: "UsuarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_Cuentas_Usuarios_UsuarioId",
                table: "Cuentas",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cuentas_Usuarios_UsuarioId",
                table: "Cuentas");

            migrationBuilder.DropIndex(
                name: "IX_Cuentas_UsuarioId",
                table: "Cuentas");

            migrationBuilder.AlterColumn<long>(
                name: "UsuarioId",
                table: "Cuentas",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "UsuarioId1",
                table: "Cuentas",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Cuentas_UsuarioId1",
                table: "Cuentas",
                column: "UsuarioId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Cuentas_Usuarios_UsuarioId1",
                table: "Cuentas",
                column: "UsuarioId1",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
