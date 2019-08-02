using ISOOU.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ISOOU.Data.Models
{
    //Refactor >> all functionality about freespots here?
    [NotMapped]
    public static class FreeSpotsCenter
    {
        public static int CalculateFreeSpotsCoeficientByYear(int yearOfBirth)
        {
            var years = FreeSpotsCenter.GetAllPossibleYears();

            Random random = new Random();

            Dictionary<int, int> coefByYear = new Dictionary<int, int>();

            foreach (var year in years)
            {
                coefByYear
                    .Add(year, random.Next(GlobalConstants.MinCoefOfAdmissionYear, GlobalConstants.MaxCoefOfAdmissionYear));
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
