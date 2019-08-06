namespace Marketplace.App.Infrastructure
{
    public class GlobalConstants
    {
        public const string AdministratorRole = "Administrator";
        public const string UserRole = "User";

        public const string ShoppingCartKey = "%ShoppingCartKey%";


        public const string PriceMinValue = "0.01";
        public const string PriceMaxValue = "79228162514264337593543950335";
        public const int MinQuantityValue = 1;
        public const int ProductNameMinLenght = 2;
        public const int ProductNameMaxLenght = 100;
        public const int ProductDescriptionMinLenght = 30;
        public const int ProductDescriptionMaxLenght = 300;

        public const int ContactUserNameMinLenght = 2;
        public const int ContactUserNameMaxLenght = 50;
        public const int ContactMessageMinLenght = 30;
        public const int ContactMessageMaxLenght = 250;

        public const int LessThanDaysIsNew = 3;
        public const string SearchCategoryDefaultValue = "All Categories";
        public const string HeadTextForFoundResult = "Found results";

        public const string DefaultPicturesPath = "wwwroot/images/users/";
    }
}
