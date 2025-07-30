using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessoriesApp.Web.ViewModels
{
    public class OrderDetailsModel
    {        
        public int Id { get; set; }
        public string OrderUserId { get; set; } = null!;
        public string OrderUserName { get; set; } = null!;
        public int? CourierId { get; set; }
        public string? CourierName { get; set; }
        public DateOnly CreatedOnOrder { get; set; }
        public int TotalCountProducts { get; set; }
        public decimal TotalPriceBGN { get; set; }        
        public decimal TotalPriceEuro => Math.Round(TotalPriceBGN / 1.95583m, 2); // Calculated, not mapped to DB
        public bool IsOrderConfirmed { get; set; } = false;

        public IEnumerable<OrderItemDetailsModel> OrderItems { get; set; } = new List<OrderItemDetailsModel>();
    }
}
