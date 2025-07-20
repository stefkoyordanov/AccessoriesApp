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
    public class OrderItem
    {
        [Key]
        public int Id { get; set; }

        [Comment("OrderId for referenced Order.")]
        [Required]
        public int? OrderId { get; set; }
        public Order? Order { get; set; }



        [Comment("Foreign key to the referenced AspNetUser.")]        
        [Required]
        public string OrderItemUserId { get; set; } = null!;

        [ForeignKey(nameof(OrderItemUserId))]
        public IdentityUser OrderItemUser { get; set; } = null!;


        [Comment("Foreign key to the referenced Accessory.")]        
        [Required]
        public int OrderItemAccessoryId { get; set; }

        [ForeignKey(nameof(OrderItemAccessoryId))]
        public Accessory OrderItemAccessory { get; set; } = null!;

        
        [Required]
        public int Quantity { get; set; }

        [Precision(18, 2)]  // Total 18 digits, 2 after the decimal point
        [Required]
        public decimal PriceBGN { get; set; }

        [Comment("Shows if the OrderItem has been fulfilled is active")]
        public bool IsOrderItemIsActive { get; set; } = true;

        [Comment("Shows if the Order has been confirmed")]
        public bool IsOrderItemConfirmed { get; set; } = false;

        
    }

}
