using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ISOOU.Services.Data.Contracts
{
    public interface ICalculatorService
    {
        Task<int> CalculateBasicScoresByCriteria(int id);

        int CalculateAdditionalScoresForNumberOfWish(int numOfWish);

        Task<int> CalculateAdditionalScoresForSchools(int candidateId, int schoolId);

        IEnumerable<int> GetAllPossibleYearsToApply();

        int CalculateCoeficientByYear(int yearOfBirth);

        int CalculateTotalScoreForTheAdmissionProcedure(int candidateId, int schoolId);

        Task<bool> EditBasicScoresForManyBrothersAndSisters(int candidateId);

        Task<bool> EditBasicScoresByCriteria(int candidateId);
    }
}
