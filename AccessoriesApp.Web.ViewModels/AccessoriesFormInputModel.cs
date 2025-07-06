using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static AccessoriesApp.Web.ViewModels.ValidationMessages.AccessoriesMessages;
using static AccessoriesApp.GCommon.ApplicationConstants;
using static AccessoriesApp.GCommon.AccessoriesConstants;

namespace AccessoriesApp.Web.ViewModels
{
    public class AccessoriesFormInputModel
    {
        // Id does not have validation, since the model is shared between Add and Edit
        // Id will be validated in the corresponding Service method
        public string Id { get; set; } = string.Empty;

        [Required(ErrorMessage = TitleRequiredMessage)]
        [MinLength(TitleMinLength, ErrorMessage = TitleMinLengthMessage)]
        [MaxLength(TitleMaxLength, ErrorMessage = TitleMaxLengthMessage)]
        public string Title { get; set; } = null!;
                
        [Required]
        [Display(Name = "Category")]
        public int CategoryId { get; set; }

        public IEnumerable<CategoryViewModel> Categories { get; set; } = new List<CategoryViewModel>();

        [Required(ErrorMessage = ReleaseDateRequiredMessage)]
        public string ReleaseDate { get; set; } = null!;
        [Required(ErrorMessage = PriceEuroRequiredMessage)]

        public string PriceBGN { get; set; } = null!;

        [Required(ErrorMessage = DescriptionRequiredMessage)]
        [MinLength(DescriptionMinLength, ErrorMessage = DescriptionMinLengthMessage)]
        [MaxLength(DescriptionMaxLength, ErrorMessage = DescriptionMaxLengthMessage)]
        public string Description { get; set; } = null!;

        public byte[]? Image { get; set; }

    }
}
