using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static AccessoriesApp.GCommon.AccessoriesConstants;

namespace AccessoriesApp.Data.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(CategoryNameMaxLength, MinimumLength = CategoryNameMinLength)]
        public string Name { get; set; } = null!;

        [Comment("Shows if Category is active")]
        public bool IsActive { get; set; } = false;

        public ICollection<Accessory> Accessories { get; set; } = new List<Accessory>();
    }
}
