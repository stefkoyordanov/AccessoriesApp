using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccessoriesApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateMigrationBGN : Migration
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
                computedColumnSql: "ROUND([PriceEuro] / 1.95583, 2)",
                comment: "Accessory price BGN",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2,
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
                comment: "Accessory price BGN",
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)",
                oldPrecision: 18,
                oldScale: 2,
                oldComputedColumnSql: "ROUND([PriceEuro] / 1.95583, 2)",
                oldComment: "Accessory price BGN");

            migrationBuilder.UpdateData(
                table: "Accessories",
                keyColumn: "Id",
                keyValue: new Guid("02b52bb0-1c2b-49a4-ba66-6d33f81d38d1"),
                column: "PriceBGN",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Accessories",
                keyColumn: "Id",
                keyValue: new Guid("16376cc6-b3e0-4bf7-a0e4-9cbd1490522c"),
                column: "PriceBGN",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Accessories",
                keyColumn: "Id",
                keyValue: new Guid("4491b6f5-2a11-4c4c-8c6b-c371f47d2152"),
                column: "PriceBGN",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Accessories",
                keyColumn: "Id",
                keyValue: new Guid("54082f99-023b-4d68-89ac-44c00a0958d0"),
                column: "PriceBGN",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Accessories",
                keyColumn: "Id",
                keyValue: new Guid("68fb84b9-ef2a-402f-b4fc-595006f5c275"),
                column: "PriceBGN",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Accessories",
                keyColumn: "Id",
                keyValue: new Guid("777634e2-3bb6-4748-8e91-7a10b70c78ac"),
                column: "PriceBGN",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Accessories",
                keyColumn: "Id",
                keyValue: new Guid("811a1a9e-61a8-4f6f-acb0-55a252c2b713"),
                column: "PriceBGN",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Accessories",
                keyColumn: "Id",
                keyValue: new Guid("844d9abd-104d-41ab-b14a-ce059779ad91"),
                column: "PriceBGN",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Accessories",
                keyColumn: "Id",
                keyValue: new Guid("ab2c3213-48a7-41ea-9393-45c60ef813e6"),
                column: "PriceBGN",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Accessories",
                keyColumn: "Id",
                keyValue: new Guid("ae50a5ab-9642-466f-b528-3cc61071bb4c"),
                column: "PriceBGN",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Accessories",
                keyColumn: "Id",
                keyValue: new Guid("bf9ff8b3-3209-4b18-9f4b-5172c45b73f9"),
                column: "PriceBGN",
                value: 0m);

            migrationBuilder.UpdateData(
                table: "Accessories",
                keyColumn: "Id",
                keyValue: new Guid("e00208b1-cb12-4bd4-8ac1-36ab62f7b4c9"),
                column: "PriceBGN",
                value: 0m);
        }
    }
}
