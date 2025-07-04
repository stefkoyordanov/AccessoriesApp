using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessoriesApp.Web.ViewModels
{
    public class AccessoriesDetailsViewModel
    {
        public string Id { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string CategoryAccessory { get; set; } = null!;
        public string ReleaseDate { get; set; } = null!;
        public string PriceBGN { get; set; } = null!;
        public string PriceEuro { get; set; } = null!;

        public string Description { get; set; } = null!;
        public byte[]? Image { get; set; }
    }
}
