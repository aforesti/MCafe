using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MCafe.Cashier.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeSchema : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "cashier");

            migrationBuilder.RenameTable(
                name: "Orders",
                newName: "Orders",
                newSchema: "cashier");

            migrationBuilder.RenameTable(
                name: "OrderItem",
                newName: "OrderItem",
                newSchema: "cashier");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameTable(
                name: "Orders",
                schema: "cashier",
                newName: "Orders");

            migrationBuilder.RenameTable(
                name: "OrderItem",
                schema: "cashier",
                newName: "OrderItem");
        }
    }
}
