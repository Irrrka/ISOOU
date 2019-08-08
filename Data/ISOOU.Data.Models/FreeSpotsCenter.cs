namespace ISOOU.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;

    using ISOOU.Common;

    //TODO Refactor >> all functionality about freespots here?
    [NotMapped]
    public static class FreeSpotsCenter
    {
        public static void GetFreeSpotsByYearsAndDistrictBySchool()
        {

        }

        public static int CalculateCoeficient(int yearOfBirth)
        {
            var years = FreeSpotsCenter.GetAllPossibleYears();

            Random random = new Random();

            Dictionary<int, int> coefByYear = new Dictionary<int, int>();

            foreach (var year in years)
            {
                var coef = year == DateTime.Now.Year - GlobalConstants.InTimeCandidate
                                ? GlobalConstants.MaxCoefOfAdmissionYear
                                : GlobalConstants.MinCoefOfAdmissionYear;
                coefByYear
                    .Add(year, coef);
            }

            return coefByYear[yearOfBirth];
        }

        public static IEnumerable<int> GetAllPossibleYears()
        {
            int initialYear = DateTime.Now.Year;
            int youngestCandidatePossibleYear = initialYear - GlobalConstants.YoungestCandidate;
            int oldestCandidatePossibleYear = initialYear - GlobalConstants.OldestCandidate;

            var possibleYears = new List<int>();
            for (int year = youngestCandidatePossibleYear; year >= oldestCandidatePossibleYear; year--)
            {
                possibleYears.Add(year);
            }

            return possibleYears;
        }
    }
}
