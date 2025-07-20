using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccessoriesApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangeFieldNameOrderItemConfirmed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOrderItemFulfilled",
                table: "OrderItems");

            migrationBuilder.AddColumn<bool>(
                name: "IsOrderItemConfirmed",
                table: "OrderItems",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "Shows if the Order has been confirmed");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOrderItemConfirmed",
                table: "OrderItems");

            migrationBuilder.AddColumn<bool>(
                name: "IsOrderItemFulfilled",
                table: "OrderItems",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "Shows if the Order has been fulfilled is active");
        }
    }
}
