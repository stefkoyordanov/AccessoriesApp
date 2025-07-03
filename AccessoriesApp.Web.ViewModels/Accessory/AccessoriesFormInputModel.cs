using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static AccessoriesApp.Data.Common.EntityConstants.AccessoriesConstants;
using static AccessoriesApp.Web.ViewModels.ValidationMessages.AccessoriesMessages;
using static AccessoriesApp.GCommon.ApplicationConstants;

namespace AccessoriesApp.Web.ViewModels.Accessory
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

        [Required(ErrorMessage = TypeAccessoryRequiredMessage)]
        [MinLength(TypeMinLength, ErrorMessage = TypeAccessoryMinLengthMessage)]
        [MaxLength(TypeMaxLength, ErrorMessage = TypeAccessoryMaxLengthMessage)]
        public string TypeAccessory { get; set; } = null!;

        [Required(ErrorMessage = ReleaseDateRequiredMessage)]
        public string ReleaseDate { get; set; } = null!;
        [Required(ErrorMessage = PriceEuroRequiredMessage)]
        public string PriceEuro { get; set; } = null!;

        [Required(ErrorMessage = DescriptionRequiredMessage)]
        [MinLength(DescriptionMinLength, ErrorMessage = DescriptionMinLengthMessage)]
        [MaxLength(DescriptionMaxLength, ErrorMessage = DescriptionMaxLengthMessage)]
        public string Description { get; set; } = null!;

        [MaxLength(ImageUrlMaxLength, ErrorMessage = ImageUrlMaxLengthMessage)]
        public string? ImageUrl { get; set; } = $"/images/{NoImageUrl}";
    }
}
