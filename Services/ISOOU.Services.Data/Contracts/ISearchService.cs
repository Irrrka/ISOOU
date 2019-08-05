namespace ISOOU.Services.Data.Contracts
{
    using System.Threading.Tasks;

    using ISOOU.Web.ViewModels.Search;

    public interface ISearchService
    {
        Task<SearchFreeSpotsResultViewModel> GetSearchResult(int districtId, int year);

        //Task<Dictionary<string, int>> GetFreeSpotsClassesBySchool(int schoolId);
    }
}
