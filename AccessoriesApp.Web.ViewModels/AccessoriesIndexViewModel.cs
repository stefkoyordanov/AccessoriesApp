using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessoriesApp.Web.ViewModels
{
    public class AccessoriesIndexViewModel
    {
        // ViewModel:
        // Describes all properties of the corresponding Entity that we want to visualize
        
        public string Id { get; set; } = null!;
        public string Title { get; set; } = null!;        
        public string CategoryAccessory { get; set; } = null!;
        public string ReleaseDate { get; set; } = null!;
        public string PriceBGN { get; set; } = null!;
        public string PriceEuro { get; set; } = null!;
        public string Description { get; set; } = null!;        
        public byte[]? Image { get; set; }

        public bool IsAuthor { get; set; } // Determines if the logged-in user is the author

        public bool IsSaved { get; set; } // Determines if the accessory is in the user's favorites
        public long SavedCount { get; set; }

    }
}



