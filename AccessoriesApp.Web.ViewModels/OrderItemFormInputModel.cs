using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessoriesApp.Web.ViewModels
{
    public class OrderItemFormInputModel
    {
        // Id does not have validation, since the model is shared between Add and Edit
        // Id will be validated in the corresponding Service method
        public int Id { get; set; }
        
        
        public int? OrderId { get; set; }
        public string? OrderItemUserId { get; set; } 
        public int OrderItemAccessoryId { get; set; }
        
        
        public string Title { get; set; } = null!;
        public string CategoryName { get; set; } = null!;
        public DateOnly ReleaseDate { get; set; }

        [Required]
        public int Quantity { get; set; } = 1;

        public decimal PriceBGN { get; set; }
        public decimal PriceEuro => Math.Round(PriceBGN / 1.95583m, 2); // Calculated, not mapped to DB

        public string Description { get; set; } = null!;
        public byte[]? Image { get; set; }

        public string AuthorUserName { get; set; } = null!;

        public bool IsAuthor { get; set; } // Show Edit/Delete buttons if true

        public bool IsSaved { get; set; } // Show Favorites button if false and user is not author
    }
}
