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
        public string CategoryName { get; set; } = null!;
        public DateOnly ReleaseDate { get; set; }
        public string PriceBGN { get; set; } = null!;
        public string PriceEuro { get; set; } = null!;

        public string Description { get; set; } = null!;
        public byte[]? Image { get; set; }

        public string AuthorUserName { get; set; } = null!;

        public bool IsAuthor { get; set; } // Show Edit/Delete buttons if true

        public bool IsSaved { get; set; } // Show Favorites button if false and user is not author
    }
}
