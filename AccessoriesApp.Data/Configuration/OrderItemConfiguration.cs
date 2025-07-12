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
    public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            // Define composite Primary Key of the Mapping Entity
            //builder
            //    .HasKey(aum => new { aum.OrderItemId });

            // Configure relation between Accessory and IdentityUser
            // The IdentityUser does not contain navigation property, as it is built-in type from the ASP.NET Core Identity
            builder
                .HasOne(aum => aum.OrderItemUser)
                .WithMany() // We do not have navigation property from the IdentityUser side
                .HasForeignKey(aum => aum.OrderItemUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure relation between UserAccessory and Accessory
            builder
                .HasOne(aum => aum.OrderItemAccessory)
                .WithMany(m => m.OrderItems)
                .HasForeignKey(aum => aum.OrderItemAccessoryId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
