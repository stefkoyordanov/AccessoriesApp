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
    public class CourierConfiguration : IEntityTypeConfiguration<Courier>
    {
        public void Configure(EntityTypeBuilder<Courier> builder)
        {
            builder.HasData(SeedCategories());
        }

        public List<Category> SeedCategories()
        {
            List<Category> categories = new List<Category>()
            {
                new Category { Id = 1, Name = "Speedy" },
                new Category { Id = 2, Name = "Econt" },
                new Category { Id = 3, Name = "Sameday" }
            };
            return categories;
        }


    }
}
