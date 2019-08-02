namespace ISOOU.Web.ViewModels
{
    using System.Collections.Generic;

    public class StatusCandidatesViewModel
    {
        public StatusCandidatesViewModel()
        {
            this.StatusCandidates = new List<StatusCandidateViewModel>();
        }

        public List<StatusCandidateViewModel> StatusCandidates { get; set; }
    }
}
