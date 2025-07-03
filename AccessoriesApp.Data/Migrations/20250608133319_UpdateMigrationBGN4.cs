using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccessoriesApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMigrationBGN4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "PriceBGN",
                table: "Accessories",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                computedColumnSql: "CAST(ROUND([PriceEuro] / 1.95583, 2) AS decimal(18,2))",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2,
                oldComputedColumnSql: "ROUND([PriceEuro] / 1.95583, 2)",
                oldComment: "Accessory price BGN");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "PriceBGN",
                table: "Accessories",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                computedColumnSql: "ROUND([PriceEuro] / 1.95583, 2)",
                comment: "Accessory price BGN",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2,
                oldComputedColumnSql: "CAST(ROUND([PriceEuro] / 1.95583, 2) AS decimal(18,2))");
        }
    }
}
