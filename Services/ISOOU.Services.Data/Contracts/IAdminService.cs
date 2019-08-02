namespace ISOOU.Services.Data.Contracts
{
    using ISOOU.Data.Models;

    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAdminService
    {
        Task<Dictionary<School, Dictionary<ClassLanguageType, List<Candidate>>>> StartAdmissionProcedure();
    }
}