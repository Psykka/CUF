using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CUF.Migrations
{
    /// <inheritdoc />
    public partial class AddSupplierToOrderInformation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SupplierId",
                table: "OrderInformation",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_OrderInformation_SupplierId",
                table: "OrderInformation",
                column: "SupplierId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderInformation_Suppliers_SupplierId",
                table: "OrderInformation",
                column: "SupplierId",
                principalTable: "Suppliers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderInformation_Suppliers_SupplierId",
                table: "OrderInformation");

            migrationBuilder.DropIndex(
                name: "IX_OrderInformation_SupplierId",
                table: "OrderInformation");

            migrationBuilder.DropColumn(
                name: "SupplierId",
                table: "OrderInformation");
        }
    }
}
