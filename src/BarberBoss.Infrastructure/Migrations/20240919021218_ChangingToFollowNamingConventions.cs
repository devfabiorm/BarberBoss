using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BarberBoss.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangingToFollowNamingConventions : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_BarberShop_BarberShopId",
                table: "Invoices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BarberShop",
                table: "BarberShop");

            migrationBuilder.RenameTable(
                name: "BarberShop",
                newName: "BarberShops");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BarberShops",
                table: "BarberShops",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_BarberShops_BarberShopId",
                table: "Invoices",
                column: "BarberShopId",
                principalTable: "BarberShops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_BarberShops_BarberShopId",
                table: "Invoices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BarberShops",
                table: "BarberShops");

            migrationBuilder.RenameTable(
                name: "BarberShops",
                newName: "BarberShop");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BarberShop",
                table: "BarberShop",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_BarberShop_BarberShopId",
                table: "Invoices",
                column: "BarberShopId",
                principalTable: "BarberShop",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
