namespace ISOOU.Services.Data.Contracts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    using ISOOU.Web.ViewModels.Search;

    public interface ISearchService
    {
        Task<SearchFreeSpotsResultViewModel> GetSearchResult(List<int> districtIds);
    }
}
