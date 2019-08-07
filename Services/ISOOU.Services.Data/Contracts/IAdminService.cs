namespace ISOOU.Services.Data.Contracts
{
    using ISOOU.Data.Models;
    using ISOOU.Services.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAdminService
    {
        Task<Dictionary<School, Dictionary<ClassProfile, List<Candidate>>>> StartAdmissionProcedure();
        Task<QuestionServiceModel> ReadLastMessage();
    }
}