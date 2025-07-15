using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AccessoriesApp.Data.Models;
using System.Reflection.Emit;

namespace AccessoriesApp.Data.Configuration
{
    public class UserAccessoriesConfiguration : IEntityTypeConfiguration<UserAccessory>
    {
        public void Configure(EntityTypeBuilder<UserAccessory> builder)
        {            

            // Define composite Primary Key of the Mapping Entity
            builder
                .HasKey(aum => new { aum.UserId, aum.AccessoryId });

            // Configure relation between Accessory and IdentityUser
            // The IdentityUser does not contain navigation property, as it is built-in type from the ASP.NET Core Identity
            builder
                .HasOne(aum => aum.User)
                .WithMany() // We do not have navigation property from the IdentityUser side
                .HasForeignKey(aum => aum.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Configure relation between UserAccessory and Accessory
            builder
                .HasOne(aum => aum.Accessory)
                .WithMany(m => m.UserAccessories)
                .HasForeignKey(aum => aum.AccessoryId)
                .OnDelete(DeleteBehavior.Restrict);

           
            
        }

        


    }

}
