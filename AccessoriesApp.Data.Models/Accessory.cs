using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

using static AccessoriesApp.GCommon.AccessoriesConstants;

namespace AccessoriesApp.Data.Models
{
    [Comment("Accessory in the system")]
    public class Accessory
    {        
        [Comment("Accessory identifier")]
        public int Id { get; set; }

        [Comment("Accessory title")]
        [Required]
        [StringLength(TitleMaxLength, MinimumLength = TitleMinLength)]
        public string Title { get; set; } = null!;

        [Comment("Accessory CategoryId")]
        [Required]
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]        
        public Category Category { get; set; } = null!;

        [Comment("Accessory release date")]
        [Required]
        public DateOnly ReleaseDate { get; set; }
                
        [Comment("Accessory price BGN")]
        [Required]
        public decimal PriceBGN { get; set; }  

        [NotMapped]
        public decimal PriceEuro => Math.Round(PriceBGN / 1.95583m, 2); // Calculated, not mapped to DB

        [Comment("Accessory description")]
        [Required]
        [StringLength(DescriptionMaxLength, MinimumLength = DescriptionMinLength)]
        public string Description { get; set; } = null!;

        [Comment("Accessory image file name")]
        [StringLength(ImageFileNameMaxLength, MinimumLength = ImageFileNameMinLength)]
        public string? ImageFileName { get; set; }

        [Comment("Accessory image file type")]
        [StringLength(TypeImageMaxLength, MinimumLength = TypeImageMinLength)]
        public string? TypeImage{ get; set; }

        [Comment("Accessory image file")]
        public byte[]? Image { get; set; }


        [Comment("Accessory AuthorId")]
        [Required]
        public string AuthorId { get; set; } = null!;


        [ForeignKey(nameof(AuthorId))]
        public IdentityUser Author { get; set; } = null!;

        
        [Comment("Shows if Accessory is deleted")]
        public bool IsDeleted { get; set; } = false;

        public ICollection<UserAccessory> UserAccessories { get; set; } = new List<UserAccessory>();

    }
}
