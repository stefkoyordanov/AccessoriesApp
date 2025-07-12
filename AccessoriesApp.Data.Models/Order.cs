using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessoriesApp.Data.Models
{
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Comment("Foreign key to the referenced AspNetUser.")]        
        [Required]        
        public string OrderUserId { get; set; } = null!;

        [ForeignKey(nameof(OrderUserId))]
        public IdentityUser OrderUser { get; set; } = null!;


        [Comment("Foreign key to the referenced OrderItem.")]        
        [Required]
        public int OrderItemId { get; set; }

        [ForeignKey(nameof(OrderItemId))]
        public OrderItem OrderItem { get; set; } = null!;

        [Required]
        public DateOnly CreatedOn { get; set; }

        [Precision(18, 2)]  // Total 18 digits, 2 after the decimal point
        [Required]
        public decimal TotalPriceBGN { get; set; }

        [NotMapped]        
        public decimal TotalPriceEuro => Math.Round(TotalPriceBGN / 1.95583m, 2); // Calculated, not mapped to DB

        [Comment("Shows if the Order has been fulfilled is active")]
        public bool IsOrderFulfilled { get; set; } = false;
    }

}
