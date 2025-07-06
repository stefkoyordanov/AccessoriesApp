namespace AccessoriesApp.Web.ViewModels
{
    public static class ValidationMessages
    {
        public static class AccessoriesMessages
        {
            // Error messages
            public const string TitleRequiredMessage = "Title is required.";
            public const string TitleMinLengthMessage = "Title must be at least 2 characters.";
            public const string TitleMaxLengthMessage = "Title cannot exceed 100 characters.";

            public const string CategoryAccessoryRequiredMessage = "Category accessory is required.";
            public const string CategoryAccessoryMinLengthMessage = "Category accessory must be at least 3 characters.";
            public const string CategoryAccessoryMaxLengthMessage = "Category accessory cannot exceed 50 characters.";

            public const string ReleaseDateRequiredMessage = "Release date is required.";
            public const string PriceEuroRequiredMessage = "Price Euro is required.";
            
            public const string DescriptionRequiredMessage = "Description is required.";
            public const string DescriptionMinLengthMessage = "Description must be at least 10 characters.";
            public const string DescriptionMaxLengthMessage = "Description cannot exceed 1000 characters.";

            public const string DurationRequiredMessage = "Duration is required.";
            public const string DurationRangeMessage = "Duration must be between 1 and 300 minutes.";
            
            public const string ImageUrlMaxLengthMessage = "Image URL cannot exceed 2048 characters.";

            public const string ServiceCreateError =
                "Fatal error occurred while adding your movie! Please try again later!";
        }

    }
}
