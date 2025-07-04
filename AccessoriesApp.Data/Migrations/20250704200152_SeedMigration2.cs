using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccessoriesApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedMigration2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Accessories",
                columns: new[] { "Id", "AuthorId", "CategoryId", "Description", "Image", "ImageFileName", "IsDeleted", "PriceBGN", "ReleaseDate", "Title", "TypeImage" },
                values: new object[] { 1, "bda68b66-7ff7-43de-88b1-705b0181666d", 6, "Размер: един\r\nМатерия: слама", null, null, false, 7.25m, new DateOnly(2005, 11, 1), "Дамско бомбе 05-0000769 S мента", null });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Accessories",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
