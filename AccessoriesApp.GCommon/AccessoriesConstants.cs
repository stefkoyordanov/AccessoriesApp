using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AccessoriesApp.GCommon
{
    public static class AccessoriesConstants
    {
        /// <summary>
        /// Accessory Title should be at least 2 characters and up to 100 characters.
        /// </summary>
        public const int TitleMinLength = 2;

        /// <summary>
        /// Accessory Title should be able to store text with length up to 100 characters.
        /// </summary>
        public const int TitleMaxLength = 100;

        public const int CategoryMinLength = 2;

        public const int CategoryMaxLength = 100;

        

        /// <summary>
        /// Accessory Description must be at least 5 characters.
        /// </summary>
        public const int DescriptionMinLength = 5;

        /// <summary>
        /// Accessory Description should be able to store text with length up to 1000 characters.
        /// </summary>
        public const int DescriptionMaxLength = 500;

        public const int ImageFileNameMinLength = 1;
        public const int ImageFileNameMaxLength = 500;

        public const int TypeImageMinLength = 1;
        public const int TypeImageMaxLength = 500;


        public const int CategoryNameMinLength = 3;
        public const int CategoryNameMaxLength = 20;

        public const int CourierNameMinLength = 3;
        public const int CourierNameMaxLength = 20;
    }


}
