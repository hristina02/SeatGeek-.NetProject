namespace SeatGeek.Common
{
   
    public static class EntityValidationConstants
    {
        public static class Category
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 30;
        }

        public static class Order
        {
           
            public const int OrderMaxNumber = 1000;
        }


        public static class Event
        {
            public const int TitleMinLength = 10;
            public const int TitleMaxLength = 50;

            public const int AddressMinLength = 15;
            public const int AddressMaxLength = 150;

            public const int CityMinLength = 2;
            public const int CityMaxLength = 30;

            public const int DescriptionMinLength = 30;
            public const int DescriptionMaxLength = 1000;

            public const int MaxCapacityConst = 1000;

            public const int ImageUrlMaxLength = 2048;

            public const string PricePerMonthMinValue = "0";
            public const string PricePerMonthMaxValue = "2000";

            public const string dateTimeFormat = "yyyy-MM-dd H:mm";
        }

        public static class Agent
        {
            public const int PhoneNumberMinLength = 7;
            public const int PhoneNumberMaxLength = 15;
        }

        public  static class Ticket
        {
           
            public const int MaxQuantity = 10000;
            public const int MaxNumberForBuy = 30;
            public const string MinPrice ="0";
            public const string MaxPrice = "1000";


        }

        public static class User
        {

            public const int FirstNameMinLength = 1;
            public const int FirstNameMaxLength = 12;

            public const int LastNameMinLength = 1;
            public const int LastNameMaxLength = 15;

            public const int PasswordMinLength = 6;
            public const int PasswordMaxLength = 100;
        }
    }
}
