﻿using AccessoriesApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessoriesApp.Data.Configuration
{
    public class CategoryConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasData(SeedCategories());
        }

        public List<Category> SeedCategories()
        {
            List<Category> categories = new List<Category>()
            {
                new Category { Id = 1, Name = "Earrings" },
                new Category { Id = 2, Name = "Necklaces" },
                new Category { Id = 3, Name = "Bags" },
                new Category { Id = 4, Name = "Glasses" },
                new Category { Id = 5, Name = "Scarves" },
                new Category { Id = 6, Name = "Beachtowels" },
                new Category { Id = 7, Name = "Purses" },
                new Category { Id = 8, Name = "Hats" }
            };
            return categories;
        }


    }
}
