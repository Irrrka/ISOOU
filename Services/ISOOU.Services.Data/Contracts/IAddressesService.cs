namespace ISOOU.Services.Data.Contracts
{
    using System.Threading.Tasks;

    using ISOOU.Services.Models;

    public interface IAddressesService
    {
        Task<AddressDetailsServiceModel> GetAddressDetailsById(int id);
    }
}
