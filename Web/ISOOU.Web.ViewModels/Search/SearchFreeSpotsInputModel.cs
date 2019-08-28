using System.ComponentModel.DataAnnotations;

namespace ISOOU.Web.ViewModels.Search
{ 
    public class SearchFreeSpotsInputModel
    {
        [Display(Name = "Район по постоянен адрес")]
        public int SelectedPermanentDistrictId { get; set; } = 0;

        [Display(Name = "Район по настоящ")]
        public int SelectedCurrentDistrictId { get; set; } = 0;

        [Display(Name = "Район по месторабота")]
        public int SelectedWorkDistrictId { get; set; } = 0;
    }
}
