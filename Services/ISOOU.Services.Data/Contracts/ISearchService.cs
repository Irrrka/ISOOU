namespace ISOOU.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ISOOU.Web.ViewModels;
    using ISOOU.Web.ViewModels.Schools;

    public interface ISearchService
    {
        Task<SearchFreeSpotsResultViewModel> GetSearchResultAsync(string district, int year);

        //Task<Dictionary<string, int>> GetFreeSpotsClassesBySchool(int schoolId);
    }
}
