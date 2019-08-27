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
        public const int InTimeCandidate = 7;
        public const int OldestCandidate = 8;
        public const int MaxNumberOfApplications = 3;
        public const string AdmittedStatusMessage = "{Name} е приет!";
        public const string NotAdmittedStatusMessage = "{Name} не е приет!";
        public const string AutomaticReply = "Изчислява се автоматично";

        // Classes
        #region Classes
        public const string EnglishLangProfile = "Английски език";
        public const string RussianLangProfile = "Руски език";
        public const string MathProfile = "Математика";
        public const string PaintProfile = "Рисуване";
        public const string MusicProfile = "Музика";
        public static readonly int FreeSpotsEnglishLangProfile = 36;
        public static readonly int FreeSpotsRussianLangProfile = 25;
        public static readonly int FreeSpotsMathProfile = 36;
        public static readonly int FreeSpotsPaintProfile = 18;
        public static readonly int FreeSpotsMusicProfile = 18;
        public static readonly int MinCoefOfAdmissionYear = 1;
        public static readonly int MaxCoefOfAdmissionYear = 1;
        #endregion

        //BasicCriterias
        public static readonly int HasNoParentCriteria = 6;
        public static readonly int HasOneParentCriteria = 3;
        public static readonly int HasVisitKGCriteria = 1;
        public static readonly int ChildrenInFamily = 3;
        public static readonly int HasManyBrothersOrSistersCriteria = 3;
        public static readonly int HasSENCriteria = 7;
        public static readonly int HasDeseasCriteria = 3;
        public static readonly int FatherHasWorkCriteria = 2;
        public static readonly int MotherHasWorkCriteria = 2;
        public static readonly int ParentPermanentCitySofiaCriteria = 2;
        public static readonly int ParentCurrentCitySofiaCriteria = 1;
        public static readonly int HasAllTheImmunizations = 1;
        //SchoolCriterias
        public static readonly int ParentHasWorkInDistrictSchoolCriteria = 1;
        public static readonly int ParentPermanentDistrictSchoolCriteria = 2;
        public static readonly int ParentCurrentDistrictSchoolCriteria = 1;

        public static readonly int FirstWishApplicationSchoolCriteria = 3;
        public static readonly int SecondWishApplicationSchoolCriteria = 2;
        public static readonly int ThirdWishApplicationSchoolCriteria = 1;

        //ExceptionMessages
        public static readonly string DistrictNotFound = "Моля изберете район";
        public static readonly string ParentNotFound = "Няма намерен родител";
        public static readonly string CandidateNotFound = "Няма намерен кандидате";
        public static readonly string UserNotFound = "Няма намерен потребител";
        public static readonly string AddressNotFound = "Няма намерен адрес";
        public static readonly string SchoolNotFound = "Няма намерени училища";
        public static readonly string UniqueParentRole = "Потребителят не може да въвежда две майки/бащи!";
        public static readonly string TwoParents = "Въведете двама родителя преди да въведете кандидат!";
        public static readonly string DocumentNotFound = "Не е намерен документ!";
        public static readonly string DocumentNotSupport = "Моля изпратете Вашето заявление в pdf-формат!";
        public static readonly string ApplicationsNotFound = "Моля въведете желаното училище преди да качите своето заявление!";

        //NullReference
        public static readonly string NullReferenceSchoolId = "School with id {0} not found.";
        public static readonly string NullReferenceDistrictId = "District with id {0} not found.";
        public static readonly string NullReferenceAddressId = "Address with id {0} not found.";
        public static readonly string NullReferenceCriteriaName = "Criteria with name {0} not found.";
        public static readonly string NullReferenceParentId = "Parent with id {0} not found.";
    }
}
