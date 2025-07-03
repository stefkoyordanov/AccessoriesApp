using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccessoriesApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMigrationBGN2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceBGN",
                table: "Accessories");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "PriceBGN",
                table: "Accessories",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                computedColumnSql: "ROUND([PriceEuro] / 1.95583, 2)",
                comment: "Accessory price BGN");
        }
    }
}
