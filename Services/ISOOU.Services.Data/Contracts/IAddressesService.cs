namespace ISOOU.Services.Data.Contracts
{
    using System.Threading.Tasks;

    using ISOOU.Data.Models;
    using ISOOU.Services.Models;

    public interface IAddressesService
    {
        Task<AddressDetailsServiceModel> GetAddressDetailsById(int id);

        Task<bool> UpdateRepository(AddressDetails addressToEdit);
    }
}
