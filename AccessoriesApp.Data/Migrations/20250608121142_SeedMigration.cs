using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AccessoriesApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class SeedMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "TypeAccessory",
                table: "Accessories",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "Accessory type",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "Accessory genre");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Accessories",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                comment: "Accessory title",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "Accessory title");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Accessories",
                type: "bit",
                nullable: false,
                defaultValue: false,
                comment: "Shows if Accessory is deleted",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldComment: "Shows if Accessory is deleted");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Accessories",
                type: "nvarchar(2048)",
                maxLength: 2048,
                nullable: true,
                comment: "Accessory image url from the image store",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true,
                oldComment: "Accessory image url from the image store");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Accessories",
                type: "nvarchar(1000)",
                maxLength: 1000,
                nullable: false,
                comment: "Accessory description",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldComment: "Accessory description");

            migrationBuilder.AddColumn<decimal>(
                name: "PriceBGN",
                table: "Accessories",
                type: "decimal(18,2)",
                precision: 18,
                scale: 2,
                nullable: false,
                defaultValue: 0m,
                comment: "Accessory price BGN");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PriceBGN",
                table: "Accessories");

            migrationBuilder.AlterColumn<string>(
                name: "TypeAccessory",
                table: "Accessories",
                type: "nvarchar(max)",
                nullable: false,
                comment: "Accessory genre",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldComment: "Accessory type");

            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Accessories",
                type: "nvarchar(max)",
                nullable: false,
                comment: "Accessory title",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldComment: "Accessory title");

            migrationBuilder.AlterColumn<bool>(
                name: "IsDeleted",
                table: "Accessories",
                type: "bit",
                nullable: false,
                comment: "Shows if Accessory is deleted",
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: false,
                oldComment: "Shows if Accessory is deleted");

            migrationBuilder.AlterColumn<string>(
                name: "ImageUrl",
                table: "Accessories",
                type: "nvarchar(max)",
                nullable: true,
                comment: "Accessory image url from the image store",
                oldClrType: typeof(string),
                oldType: "nvarchar(2048)",
                oldMaxLength: 2048,
                oldNullable: true,
                oldComment: "Accessory image url from the image store");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Accessories",
                type: "nvarchar(max)",
                nullable: false,
                comment: "Accessory description",
                oldClrType: typeof(string),
                oldType: "nvarchar(1000)",
                oldMaxLength: 1000,
                oldComment: "Accessory description");
        }
    }
}
