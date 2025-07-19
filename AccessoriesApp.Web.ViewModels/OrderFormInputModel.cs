using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessoriesApp.Web.ViewModels
{
    public class OrderFormInputModel
    {
        public int Id { get; set; }
        public IEnumerable<OrderItemDetailsModel> OrderItems { get; set; } = new List<OrderItemDetailsModel>();

        [Required]        
        public int CourierId { get; set; }
        public IEnumerable<CourierViewModel> Couriers { get; set; } = new List<CourierViewModel>();

    }

}
