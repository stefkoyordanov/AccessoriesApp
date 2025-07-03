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

        [Comment("Accessory type")]
        [Required]
        public int CategoryId { get; set; }

        [ForeignKey(nameof(CategoryId))]
        public Category Category { get; set; } = null!;

        [Comment("Accessory release date")]
        public DateOnly ReleaseDate { get; set; }
                
        [Comment("Accessory price BGN")]
        public decimal PriceBGN { get; private set; }  // Mapped to computed column

        [NotMapped]
        public decimal PriceEuro => PriceBGN / 1.95583m; // Calculated, not mapped to DB


        [Comment("Accessory description")]
        [Required]
        [StringLength(DescriptionMinLength, MinimumLength = DescriptionMaxLength)]
        public string Description { get; set; } = null!;

        [Comment("Accessory image type")]
        public string? TypeImage{ get; set; }

        [Comment("Accessory image file")]
        public byte[] Image { get; set; } 

        // TODO: Extract the property with Id to BaseDeletableModel
        [Comment("Shows if Accessory is deleted")]
        public bool IsDeleted { get; set; }

    }
}
