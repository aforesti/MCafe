using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MCafe.Cashier.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class orderItemId : Migration
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

            migrationBuilder.DropColumn(
                name: "Id",
                schema: "cashier",
                table: "OrderItem");

            migrationBuilder.AlterColumn<Guid>(
                name: "OrderId",
                schema: "cashier",
                table: "OrderItem",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddPrimaryKey(
                name: "PK_OrderItem",
                schema: "cashier",
                table: "OrderItem",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Orders_OrderId",
                schema: "cashier",
                table: "OrderItem",
                column: "OrderId",
                principalSchema: "cashier",
                principalTable: "Orders",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Orders_OrderId",
                schema: "cashier",
                table: "OrderItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_OrderItem",
                schema: "cashier",
                table: "OrderItem");

            migrationBuilder.AlterColumn<Guid>(
                name: "OrderId",
                schema: "cashier",
                table: "OrderItem",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

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

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Orders_OrderId",
                schema: "cashier",
                table: "OrderItem",
                column: "OrderId",
                principalSchema: "cashier",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
