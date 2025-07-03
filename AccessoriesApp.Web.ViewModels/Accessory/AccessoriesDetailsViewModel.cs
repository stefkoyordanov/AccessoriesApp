using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessoriesApp.Web.ViewModels.Accessory
{
    public class AccessoriesDetailsViewModel
    {
        public string Id { get; set; } = null!;
        public string Title { get; set; } = null!;
        public string TypeAccessory { get; set; } = null!;
        public string ReleaseDate { get; set; } = null!;
        public string PriceEuro { get; set; } = null!;

        public string Description { get; set; } = null!;
        public string? ImageUrl { get; set; }
    }
}
