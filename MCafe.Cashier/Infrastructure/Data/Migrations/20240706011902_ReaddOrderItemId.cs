using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MCafe.Cashier.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class ReaddOrderItemId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Orders_OrderId",
                schema: "cashier",
                table: "OrderItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderItem",
                schema: "cashier",
                table: "OrderItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Orders",
                schema: "cashier",
                table: "Orders");

            migrationBuilder.RenameTable(
                name: "Orders",
                schema: "cashier",
                newName: "Order",
                newSchema: "cashier");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                schema: "cashier",
                table: "OrderItem",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderItem",
                schema: "cashier",
                table: "OrderItem",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Order",
                schema: "cashier",
                table: "Order",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Order_OrderId",
                schema: "cashier",
                table: "OrderItem",
                column: "OrderId",
                principalSchema: "cashier",
                principalTable: "Order",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Order_OrderId",
                schema: "cashier",
                table: "OrderItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderItem",
                schema: "cashier",
                table: "OrderItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Order",
                schema: "cashier",
                table: "Order");

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "cashier",
                table: "OrderItem");

            migrationBuilder.RenameTable(
                name: "Order",
                schema: "cashier",
                newName: "Orders",
                newSchema: "cashier");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderItem",
                schema: "cashier",
                table: "OrderItem",
                column: "ProductId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Orders",
                schema: "cashier",
                table: "Orders",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Orders_OrderId",
                schema: "cashier",
                table: "OrderItem",
                column: "OrderId",
                principalSchema: "cashier",
                principalTable: "Orders",
                principalColumn: "Id");
        }
    }
}
