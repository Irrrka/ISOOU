namespace ISOOU.Web.ViewModels.Users
{
    using System.ComponentModel.DataAnnotations;
    using ISOOU.Common;
    using ISOOU.Data.Models;
    using ISOOU.Services.Mapping;
    using ISOOU.Services.Models;

    public class EditCandidateViewModel : IMapFrom<CandidateServiceModel>, IMapTo<CandidateServiceModel>
    {
        public int Id { get; set; }

        public string FirstName { get; set; }

        public string MiddleName { get; set; }

        public string LastName { get; set; }

        public string UCN { get; set; }

        public int YearOfBirth { get; set; }

        public string KinderGarten { get; set; }

        public bool SEN { get; set; }

        public bool Desease { get; set; }

    }
}