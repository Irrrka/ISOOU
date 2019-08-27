namespace ISOOU.Services.Data.Contracts
{
    using System.Threading.Tasks;

    public interface IAdminService
    {
        Task<bool> AdmissionProcedure();

        string GetProcedureStatus();

        Task<bool> RevertAdmissionProcedure();
    }
}