using AccessoriesApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessoriesApp.Data.Configuration
{
    public class AccessoriesConfiguration : IEntityTypeConfiguration<Accessory>
    {
        public void Configure(EntityTypeBuilder<Accessory> builder)
        {
            /*
            // Define constraints for the PriceBGN column
            builder
                .Property(m => m.PriceBGN)
                .IsRequired()
                .HasPrecision(18, 2); // (precision: total digits, scale: decimal places)
            */

            //builder.HasData(SeedAccessories());
        }

        
        
        public List<Accessory> SeedAccessories()
        {
            List<Accessory> movies = new List<Accessory>()
            {
                new Accessory()
                {
                    Id = 1,
                    Title = "Дамско бомбе 05-0000769 S мента",
                    CategoryId = 6,
                    ReleaseDate = new DateOnly(2005, 11, 01),
                    PriceBGN = 7.25m,
                    Description = "Размер: един\r\nМатерия: слама",
                    AuthorId="bda68b66-7ff7-43de-88b1-705b0181666d",
                    IsDeleted = false

                } //,
                /*
                new Accessory()
                {
                    Id = 2,
                    Title = "Дамска раница16-0007678 бежова",
                    TypeAccessory = "Bags",
                    ReleaseDate = new DateOnly(2001, 05, 01),
                    PriceEuro = 42.52m,
                    Description = "Размери:\r\nВисочина: 28 см,\r\nДължина: 16 см,\r\nШирина: 9 см.\r\n",
                    ImageUrl = "verde_1743161112.jpg"
                },
                new Accessory()
                {
                    Id = Guid.Parse("68fb84b9-ef2a-402f-b4fc-595006f5c275"),
                    Title = "Дамска чанта 16-0007365 светло синя",
                    TypeAccessory = "Bags",
                    ReleaseDate = new DateOnly(2010, 07, 16),
                    PriceEuro = 52.52m,
                    Description = "Размери:\r\nВисочина: 38 см,\r\nДължина: 30 см,\r\nШирина: 12 см.",
                    ImageUrl = "verde_1743160545.jpg"
                },
                new Accessory()
                {
                    Id = Guid.Parse("02b52bb0-1c2b-49a4-ba66-6d33f81d38d1"),
                    Title = "Дамска чанта плик 01-0001795 лилава",
                    TypeAccessory = "Bags",
                    ReleaseDate = new DateOnly(2008, 07, 18),
                    PriceEuro = 35.52m,
                    Description = "Размери:\r\nВисочина: 22 см,\r\nДължина: 28 см,\r\nШирина: 3 см.",
                    ImageUrl = "verde_1743160301.jpg"
                },
                new Accessory()
                {
                    Id = Guid.Parse("16376cc6-b3e0-4bf7-a0e4-9cbd1490522c"),
                    Title = "Слънчеви очила HAVVS - преливащи лещи, преливаща метална рамка",
                    TypeAccessory = "Glasses",
                    ReleaseDate = new DateOnly(2014, 11, 07),
                    PriceEuro = 45.52m,
                    Description = "Форма на очила: правоъгълник.\r\nЦвят на леща: преливащ, сиво-кафяв нюанс.\r\nЦвят на рамка: сребрист.\r\nЗащита: поляризация. UV400",
                    ImageUrl = "slancevi-ocila.jpg"
                },
                new Accessory()
                {
                    Id = Guid.Parse("811a1a9e-61a8-4f6f-acb0-55a252c2b713"),
                    Title = "Мъжки слънчеви очила Тhom Richard с правоъгълна форма",
                    TypeAccessory = "Glasses",
                    ReleaseDate = new DateOnly(2009, 12, 18),
                    PriceEuro = 42.47m,
                    Description = "Характеристики на очилата\r\nФорма на очила: правоъгълна.\r\nЦвят на леща: черен.\r\nЦвят на рамка: черен.\r\nМатериал на рамка: метал.\r\nЗащита: поляризация.\r\nНомер: TR8999.",
                    ImageUrl = "mazki-slancevi-ocila-matrix-aviator"
                },
                new Accessory()
                {
                    Id = Guid.Parse("ab2c3213-48a7-41ea-9393-45c60ef813e6"),
                    Title = "Мъжки шал",
                    TypeAccessory = "Scarves",
                    ReleaseDate = new DateOnly(1997, 12, 19),
                    PriceEuro = 12.54m,
                    Description = "Цвят: Черно\r\nМатериал: Памук, Акрил\r\nРазмери: 31см. X 188см.\r\nСтил:  Ежедневен\r\nПол: Мъж",
                    ImageUrl = "mazki-sal-mazki-shal.jpeg"
                },
                new Accessory()
                {
                    Id = Guid.Parse("844d9abd-104d-41ab-b14a-ce059779ad91"),
                    Title = "Мъжки двулицев шал",
                    TypeAccessory = "Scarves",
                    ReleaseDate = new DateOnly(1999, 03, 31),
                    PriceEuro = 15.45m,
                    Description = "Цвят: Син\r\nМатериал: Памук, Акрил\r\nРазмери: 31см. X 188см.\r\nСтил:  Ежедневен\r\nПол: Мъж",
                    ImageUrl = "mazki-mazki-image.jpeg"
                },
                new Accessory()
                {
                    Id = Guid.Parse("54082f99-023b-4d68-89ac-44c00a0958d0"),
                    Title = "Плажна кърпа 61-0000027 оранжева",
                    TypeAccessory = "Beachtowels",
                    ReleaseDate = new DateOnly(1994, 07, 06),
                    PriceEuro = 17.56m,
                    Description = "Размери:\r\nВисочина: 160.00 см,\r\nШирина: 90.00 см.\r\n\r\nСъстав: памук",
                    ImageUrl = "verde_1747042025_2.jpg"
                },
                new Accessory()
                {
                    Id = Guid.Parse("bf9ff8b3-3209-4b18-9f4b-5172c45b73f9"),
                    Title = "Плажна кърпа 61-0000026 зелена",
                    TypeAccessory = "Beachtowels",
                    ReleaseDate = new DateOnly(2000, 05, 05),
                    PriceEuro = 19.52m,
                    Description = "Размери:\r\nВисочина: 160.00 см,\r\nШирина: 90.00 см.\r\n\r\nСъстав: памук",
                    ImageUrl = "verde_1747042020_2.jpg"
                },
                new Accessory()
                {
                    Id = Guid.Parse("e00208b1-cb12-4bd4-8ac1-36ab62f7b4c9"),
                    Title = "Дамско потртмоне 18-1291 бронз",
                    TypeAccessory = "Purses",
                    ReleaseDate = new DateOnly(1994, 09, 23),
                    PriceEuro = 18.78m,
                    Description = "Джоб/ове за хартиени пари: Джоб за монети: Вътрешни джобове.\r\nДжоб/ове за кредитни карти.Джоб за лична карта.\r\nМатериал: Екологична синтетична кожа.\r\n​Предлага се в подаръчна кутия.\r\n\r\nРазмери:\r\nВисочина: 19 см,\r\nДължина: 10 см,\r\nШирина: 3 см.",
                    ImageUrl = "verde_1730297565.jpg"
                },
                new Accessory()
                {
                    Id = Guid.Parse("4491b6f5-2a11-4c4c-8c6b-c371f47d2152"),
                    Title = "Pulp Fiction",
                    TypeAccessory = "Purses",
                    ReleaseDate = new DateOnly(1994, 10, 14),
                    PriceEuro = 16.47m,
                    Description = "Размери:\r\nВисочина: 9 см,\r\nДължина: 10 см,\r\nШирина: 2 см.\r\n\r\nДжоб за хартиени пари.\r\nДжоб за монети. Вътрешни джобове. Джоб за кредитни карти. Джоб за лична карта.\r\nМатериал: Екологична синтетична кожа.\r\nПредлага се в подаръчна кутия.",
                    ImageUrl = "verde_1730297433.jpg"
                }

                */
            };

            return movies;

                
        }





    }

}
