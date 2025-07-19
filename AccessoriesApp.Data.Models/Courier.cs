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
    public class Courier
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(CourierNameMaxLength, MinimumLength = CourierNameMinLength)]
        public string Name { get; set; } = null!;

        [Comment("Shows if Category is active")]
        public bool IsActive { get; set; } = true;

        public ICollection<Order> Orders { get; set; } = new List<Order>();
    }
}
