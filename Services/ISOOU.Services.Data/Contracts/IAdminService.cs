namespace ISOOU.Services.Data.Contracts
{
    using ISOOU.Data.Models;
    using ISOOU.Services.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAdminService
    {
        Task<Dictionary<School, List<SchoolCandidate>>> StartAdmissionProcedure();

        Task<QuestionServiceModel> ReadLastMessage();
    }
}