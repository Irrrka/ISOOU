namespace ISOOU.Services.Data.Contracts
{
    using ISOOU.Data.Models;
    using ISOOU.Services.Models;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public interface IAdminService
    {
        Task<bool> AdmissionProcedure();

        string GetProcedureStatus();

        Task<bool> RevertAdmissionProcedure();
    }
}