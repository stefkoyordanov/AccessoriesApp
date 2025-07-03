using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AccessoriesApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Accessories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, comment: "Accessory identifier"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Accessory title"),
                    TypeAccessory = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Accessory genre"),
                    ReleaseDate = table.Column<DateOnly>(type: "date", nullable: false, comment: "Accessory release date"),
                    PriceEuro = table.Column<decimal>(type: "decimal(18,2)", nullable: false, comment: "Accessory price Euro"),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Accessory description"),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true, comment: "Accessory image url from the image store"),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false, comment: "Shows if Accessory is deleted")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Accessories", x => x.Id);
                },
                comment: "Accessory in the system");

            migrationBuilder.InsertData(
                table: "Accessories",
                columns: new[] { "Id", "Description", "ImageUrl", "IsDeleted", "PriceEuro", "ReleaseDate", "Title", "TypeAccessory" },
                values: new object[,]
                {
                    { new Guid("02b52bb0-1c2b-49a4-ba66-6d33f81d38d1"), "Размери:\r\nВисочина: 22 см,\r\nДължина: 28 см,\r\nШирина: 3 см.", "verde_1743160301.jpg", false, 35.52m, new DateOnly(2008, 7, 18), "Дамска чанта плик 01-0001795 лилава", "Bags" },
                    { new Guid("16376cc6-b3e0-4bf7-a0e4-9cbd1490522c"), "Форма на очила: правоъгълник.\r\nЦвят на леща: преливащ, сиво-кафяв нюанс.\r\nЦвят на рамка: сребрист.\r\nЗащита: поляризация. UV400", "slancevi-ocila.jpg", false, 45.52m, new DateOnly(2014, 11, 7), "Слънчеви очила HAVVS - преливащи лещи, преливаща метална рамка", "Glasses" },
                    { new Guid("4491b6f5-2a11-4c4c-8c6b-c371f47d2152"), "Размери:\r\nВисочина: 9 см,\r\nДължина: 10 см,\r\nШирина: 2 см.\r\n\r\nДжоб за хартиени пари.\r\nДжоб за монети. Вътрешни джобове. Джоб за кредитни карти. Джоб за лична карта.\r\nМатериал: Екологична синтетична кожа.\r\nПредлага се в подаръчна кутия.", "verde_1730297433.jpg", false, 16.47m, new DateOnly(1994, 10, 14), "Pulp Fiction", "Purses" },
                    { new Guid("54082f99-023b-4d68-89ac-44c00a0958d0"), "Размери:\r\nВисочина: 160.00 см,\r\nШирина: 90.00 см.\r\n\r\nСъстав: памук", "verde_1747042025_2.jpg", false, 17.56m, new DateOnly(1994, 7, 6), "Плажна кърпа 61-0000027 оранжева", "Beachtowels" },
                    { new Guid("68fb84b9-ef2a-402f-b4fc-595006f5c275"), "Размери:\r\nВисочина: 38 см,\r\nДължина: 30 см,\r\nШирина: 12 см.", "verde_1743160545.jpg", false, 52.52m, new DateOnly(2010, 7, 16), "Дамска чанта 16-0007365 светло синя", "Bags" },
                    { new Guid("777634e2-3bb6-4748-8e91-7a10b70c78ac"), "Размери:\r\nВисочина: 28 см,\r\nДължина: 16 см,\r\nШирина: 9 см.\r\n", "verde_1743161112.jpg", false, 42.52m, new DateOnly(2001, 5, 1), "Дамска раница16-0007678 бежова", "Bags" },
                    { new Guid("811a1a9e-61a8-4f6f-acb0-55a252c2b713"), "Характеристики на очилата\r\nФорма на очила: правоъгълна.\r\nЦвят на леща: черен.\r\nЦвят на рамка: черен.\r\nМатериал на рамка: метал.\r\nЗащита: поляризация.\r\nНомер: TR8999.", "mazki-slancevi-ocila-matrix-aviator", false, 42.47m, new DateOnly(2009, 12, 18), "Мъжки слънчеви очила Тhom Richard с правоъгълна форма", "Glasses" },
                    { new Guid("844d9abd-104d-41ab-b14a-ce059779ad91"), "Цвят: Син\r\nМатериал: Памук, Акрил\r\nРазмери: 31см. X 188см.\r\nСтил:  Ежедневен\r\nПол: Мъж", "mazki-mazki-image.jpeg", false, 15.45m, new DateOnly(1999, 3, 31), "Мъжки двулицев шал", "Scarves" },
                    { new Guid("ab2c3213-48a7-41ea-9393-45c60ef813e6"), "Цвят: Черно\r\nМатериал: Памук, Акрил\r\nРазмери: 31см. X 188см.\r\nСтил:  Ежедневен\r\nПол: Мъж", "mazki-sal-mazki-shal.jpeg", false, 12.54m, new DateOnly(1997, 12, 19), "Мъжки шал", "Scarves" },
                    { new Guid("ae50a5ab-9642-466f-b528-3cc61071bb4c"), "Размер: един\r\nМатерия: слама", "verde_1743160657.jpg", false, 7.25m, new DateOnly(2005, 11, 1), "Дамско бомбе 05-0000769 S мента", "Hats" },
                    { new Guid("bf9ff8b3-3209-4b18-9f4b-5172c45b73f9"), "Размери:\r\nВисочина: 160.00 см,\r\nШирина: 90.00 см.\r\n\r\nСъстав: памук", "verde_1747042020_2.jpg", false, 19.52m, new DateOnly(2000, 5, 5), "Плажна кърпа 61-0000026 зелена", "Beachtowels" },
                    { new Guid("e00208b1-cb12-4bd4-8ac1-36ab62f7b4c9"), "Джоб/ове за хартиени пари: Джоб за монети: Вътрешни джобове.\r\nДжоб/ове за кредитни карти.Джоб за лична карта.\r\nМатериал: Екологична синтетична кожа.\r\n​Предлага се в подаръчна кутия.\r\n\r\nРазмери:\r\nВисочина: 19 см,\r\nДължина: 10 см,\r\nШирина: 3 см.", "verde_1730297565.jpg", false, 18.78m, new DateOnly(1994, 9, 23), "Дамско потртмоне 18-1291 бронз", "Purses" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Accessories");
        }
    }
}
