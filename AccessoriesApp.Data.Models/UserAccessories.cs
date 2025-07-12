using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessoriesApp.Data.Models
{
    public class UserAccessories
    {
        [Comment("Foreign key to the referenced AspNetUser. Part of the entity composite PK.")]
        [Key]
        [Required]
        public string UserId { get; set; } = null!;

        public IdentityUser User { get; set; } = null!;

        [Comment("Foreign key to the referenced Accessory. Part of the entity composite PK.")]
        [Key]
        [Required]
        public int AccessoryId { get; set; }

        public Accessory Accessory { get; set; } = null!;
    }
}
