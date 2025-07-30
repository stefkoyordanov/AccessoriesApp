using AccessoriesApp.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace AccessoriesApp.Data.Configuration
{
    public class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {

            builder
                .Property(e => e.DateInput)
                .HasDefaultValueSql("GETDATE()");

            // Define composite Primary Key of the Mapping Entity
            //builder
            //    .HasKey(aum => new { aum.OrderItemId });

            // Configure relation between Accessory and IdentityUser
            // The IdentityUser does not contain navigation property, as it is built-in type from the ASP.NET Core Identity
            builder
                .HasOne(aum => aum.OrderUser)
                .WithMany() // We do not have navigation property from the IdentityUser side
                .HasForeignKey(aum => aum.OrderUserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure relation between Order and OrderItems
            builder
                //.HasOne(aum => aum.or)
                .HasMany(aum => aum.OrderItems)
                //.WithMany(m => m.Order)
                .WithOne(one => one.Order)
                .HasForeignKey(aum => aum.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure relation between Order and Courier
            builder
                .HasOne(aum => aum.Courier)
                //.HasMany(aum => aum.OrderItems)
                .WithMany(m => m.Orders)
                //.WithOne(one => one.Order)
                .HasForeignKey(aum => aum.CourierId)
                .OnDelete(DeleteBehavior.Restrict);


        }
    }
}
