using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CUF.Migrations
{
    /// <inheritdoc />
    public partial class AddOrderModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Anexos_Products_ProductModelId",
                table: "Anexos");

            migrationBuilder.DropForeignKey(
                name: "FK_CategoryModel_Suppliers_SupplierModelId",
                table: "CategoryModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CategoryModel",
                table: "CategoryModel");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Anexos",
                table: "Anexos");

            migrationBuilder.RenameTable(
                name: "CategoryModel",
                newName: "Category");

            migrationBuilder.RenameTable(
                name: "Anexos",
                newName: "Attachment");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryModel_SupplierModelId",
                table: "Category",
                newName: "IX_Category_SupplierModelId");

            migrationBuilder.RenameIndex(
                name: "IX_CategoryModel_Name",
                table: "Category",
                newName: "IX_Category_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Anexos_ProductModelId",
                table: "Attachment",
                newName: "IX_Attachment_ProductModelId");

            migrationBuilder.AddColumn<int>(
                name: "ProductModelId",
                table: "Suppliers",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Category",
                table: "Category",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Attachment",
                table: "Attachment",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "OrderInformation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    OrderId = table.Column<int>(type: "int", nullable: true),
                    ProductId = table.Column<int>(type: "int", nullable: true),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderInformation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderInformation_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OrderInformation_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Suppliers_ProductModelId",
                table: "Suppliers",
                column: "ProductModelId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderInformation_OrderId",
                table: "OrderInformation",
                column: "OrderId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderInformation_ProductId",
                table: "OrderInformation",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_UserId",
                table: "Orders",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Attachment_Products_ProductModelId",
                table: "Attachment",
                column: "ProductModelId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Category_Suppliers_SupplierModelId",
                table: "Category",
                column: "SupplierModelId",
                principalTable: "Suppliers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Suppliers_Products_ProductModelId",
                table: "Suppliers",
                column: "ProductModelId",
                principalTable: "Products",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attachment_Products_ProductModelId",
                table: "Attachment");

            migrationBuilder.DropForeignKey(
                name: "FK_Category_Suppliers_SupplierModelId",
                table: "Category");

            migrationBuilder.DropForeignKey(
                name: "FK_Suppliers_Products_ProductModelId",
                table: "Suppliers");

            migrationBuilder.DropTable(
                name: "OrderInformation");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Suppliers_ProductModelId",
                table: "Suppliers");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Category",
                table: "Category");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Attachment",
                table: "Attachment");

            migrationBuilder.DropColumn(
                name: "ProductModelId",
                table: "Suppliers");

            migrationBuilder.RenameTable(
                name: "Category",
                newName: "CategoryModel");

            migrationBuilder.RenameTable(
                name: "Attachment",
                newName: "Anexos");

            migrationBuilder.RenameIndex(
                name: "IX_Category_SupplierModelId",
                table: "CategoryModel",
                newName: "IX_CategoryModel_SupplierModelId");

            migrationBuilder.RenameIndex(
                name: "IX_Category_Name",
                table: "CategoryModel",
                newName: "IX_CategoryModel_Name");

            migrationBuilder.RenameIndex(
                name: "IX_Attachment_ProductModelId",
                table: "Anexos",
                newName: "IX_Anexos_ProductModelId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CategoryModel",
                table: "CategoryModel",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Anexos",
                table: "Anexos",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Anexos_Products_ProductModelId",
                table: "Anexos",
                column: "ProductModelId",
                principalTable: "Products",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryModel_Suppliers_SupplierModelId",
                table: "CategoryModel",
                column: "SupplierModelId",
                principalTable: "Suppliers",
                principalColumn: "Id");
        }
    }
}
