namespace ISOOU.Common
{
    public static class GlobalConstants
    {
        // Roles
        public const string AdministratorRoleName = "Administrator";
        public const string DirectorRoleName = "Director";
        public const string UserRoleName = "User";
        //AdminRoleDetails
        public const string AdministratorUsername = "Admin";
        public const string AdministratorEmail = "admin@admin.com";
        public const string AdministratorPassword = "Parol@123";
        public const string AdministratorFullName = "AdminAdminov";

        // Schools
        public const int YoungestCandidate = 6;
        public const int OldestCandidate = 8;

        // Classes
        public const string EnglishLanguage = "Английски език";
        public const string SpanishLanguage = "Испански език";
        public const string ChineseLanguage = "Китайски език";
        public const string RussianLanguage = "Руски език";
        public static readonly int FreeSpotsEnglishLanguage = 36;
        public static readonly int FreeSpotsSpanishLanguage = 25;
        public static readonly int FreeSpotsChineseLanguage = 9;
        public static readonly int FreeSpotsRussianLanguage = 18;
        public static readonly int MinCoefOfAdmissionYear = 1;
        public static readonly int MaxCoefOfAdmissionYear = 2;

        //Criterias
        public static readonly int HasNoParentCriteria = 6;
        public static readonly int HasOneParentCriteria = 3;
        public static readonly int HasVisitKGCriteria = 1;
        public static readonly int ChildrenInFamily = 3;
        public static readonly int HasManyBrothersOrSistersCriteria = 3;
        public static readonly int HasSENCriteria = 7;
        public static readonly int HasDeseasCriteria = 3;
        public static readonly int ParentHasWorkCriteria = 2;
        public static readonly int ParentHasWorkInDistrictCriteria = 1;
        public static readonly int ParentPermanentCitySofiaCriteria = 2;
        public static readonly int ParentCurrentCitySofiaCriteria = 1;
        public static readonly int ParentPermanentDistrictCriteria = 2;
        public static readonly int ParentCurrentDistrictCriteria = 1;

        // ExceptionMessages
        public static readonly string DistrictNotFound = "Моля изберете район";
        public static readonly string ParentNotFound = "Parent Not Found";
        public static readonly string CandidateNotFound = "Candidate Not Found";
        public static readonly string UserNotFound = "User Not Found";
        public static readonly string AddressNotFound = "Address Not Found";
    }
}
