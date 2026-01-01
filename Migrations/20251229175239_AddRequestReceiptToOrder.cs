using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AQCartMvc.Migrations
{
    /// <inheritdoc />
    public partial class AddRequestReceiptToOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "RequestReceipt",
                table: "Orders",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequestReceipt",
                table: "Orders");
        }
    }
}
