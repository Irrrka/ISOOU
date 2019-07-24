using System;

namespace ISOOU.Common
{
    public static class GlobalConstants
    {
        // Roles
        public const string AdministratorRoleName = "Administrator";
        public const string DirectorRoleName = "Director";
        public const string UserRoleName = "Family";
        //AdminRoleDetails
        public const string AdministratorUsername = "Admin";
        public const string AdministratorEmail = "admin@admin.com";
        public const string AdministratorPassword = "Parol@123";
        public const string AdministratorFullName = "AdminAdminov";

        // Schools
        public const int YoungestCandidate = 6;
        public const int OldestCandidate = 10;

        // Classes
        public const string EnglishLanguage = "Английски език";
        public const string SpanishLanguage = "Испански език";
        public const string ChineseLanguage = "Китайски език";
        public const string RussianLanguage = "Руски език";
        public static readonly int InitialSpots = 18;
        public static readonly int MinCoefOfAdmissionYear = 0;
        public static readonly int MaxCoefOfAdmissionYear = 4;

        //Criterias
        public static readonly int HasNoParentCriteria = 6;
        public static readonly int HasOneParentCriteria = 3;
        public static readonly int HasVisitKGCriteria = 1;
        public static readonly int HasManyBrothersorSistersCriteria = 2;
        public static readonly int ChilrenNumber = 3;
        public static readonly int HasManyBrothersOrSistersCriteria = 3;
        public static readonly int HasSENCriteria = 7;
        public static readonly int HasHronicDeseasCriteria = 3;
        public static readonly int ParentHasWorkCriteria = 2;
        public static readonly int ParentHasWorkInDistrictCriteria = 1;
        public static readonly int ParentPermanentCitySofia = 2;
        public static readonly int ParentCurrentCitySofia = 1;

        // ExceptionMessages
        public static readonly string NullReferenceDistrictName = "Моля изберете район";
    }
}
