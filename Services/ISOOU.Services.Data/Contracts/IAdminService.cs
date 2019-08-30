namespace ISOOU.Services.Data.Contracts
{
    using ISOOU.Services.Models;
    using System.Threading.Tasks;

    public interface IAdminService
    {
        Task<bool> AdmissionProcedure();

        Task<AdmissionProcedureServiceModel> GetLastProcedure();

        Task<bool> RevertAdmissionProcedure();
    }
}