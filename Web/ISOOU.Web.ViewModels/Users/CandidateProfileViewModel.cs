namespace ISOOU.Web.Areas.Users.Models
{
    using System.ComponentModel.DataAnnotations;

    using ISOOU.Data.Models;
    using ISOOU.Services.Mapping;

    public class CandidateProfileViewModel : IMapFrom<Candidate>
    {
       
        public string FirstName { get; set; }

       
        public string MiddleName { get; set; }

        
        public string LastName { get; set; }

        
        public int YearOfBirth { get; set; }

       
        public string KinderGarten { get; set; }

        
        public string SEN { get; set; }

      
        public string Desease { get; set; }
    }
}