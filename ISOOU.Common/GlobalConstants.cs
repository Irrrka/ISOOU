using System;

namespace ISOOU.Common
{
    public static class GlobalConstants
    {
        // Roles
        public const string AdministratorRoleName = "Administrator";
        public const string DirectorRoleName = "Director";
        public const string UserRoleName = "Family";

        // Schools
        public const int YoungestCandidate = 6;
        public const int OldestCandidate = 10;

        // Classes
        public const string EnglishLanguage = "Английски език";
        public const string SpanishLanguage = "Испански език";
        public const string ChineseLanguage = "Китайски език";
        public const string RussianLanguage = "Руски език";
        public static readonly int InitialSpots = 18;
        public static readonly string ACode = "A";
        public static readonly string BCode = "B";
        public static readonly string CCode = "C";
        public static readonly string DCode = "D";
        public static readonly int MinCoefOfAdmissionYear = 0;
        public static readonly int MaxCoefOfAdmissionYear = 4;

        // ExceptionMessages
        public static readonly string NullReferenceDistrictName = "Моля изберете район";
    }
}
