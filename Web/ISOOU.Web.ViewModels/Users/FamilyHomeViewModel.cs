namespace ISOOU.Web.ViewModels.Users
{
    using System.Collections.Generic;

    public class FamilyHomeViewModel
    {
        public FamilyHomeViewModel()
        {
            this.Parents = new List<ParentsHomeViewModel>();
            this.Candidates = new List<CandidatesHomeViewModel>();
        }

        public ICollection<ParentsHomeViewModel> Parents { get; set; }

        public ICollection<CandidatesHomeViewModel> Candidates { get; set; }

    }
}
