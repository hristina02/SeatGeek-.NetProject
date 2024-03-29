﻿ namespace SeatGeek.Common
{
    public class GeneralApplicationConstants
    {
        public const int ReleaseYear = 2024;

        public const int DefaultPage =1;
        public const int EntitiesPerPage = 3;


        public const string AdminAreaName = "Admin";
        public const string AdminRoleName = "Administrator";
        public const string DevelopmentAdminEmail = "administrator@gmail.com";

        public const string UsersCacheKey = "UsersCache";
        public const string CategoriesCacheKey = "RentsCache";
        public const int UsersCacheDurationMinutes = 5;
        public const int CategoriesCacheDurationMinutes = 10;

        public const string OnlineUsersCookieName = "IsOnline";
        public const int LastActivityBeforeOfflineMinutes = 10;
    }
}
