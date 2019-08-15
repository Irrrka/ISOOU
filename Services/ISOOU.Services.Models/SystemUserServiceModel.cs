namespace ISOOU.Services.Models
{
    using System;
    using System.Collections.Generic;

    using ISOOU.Data.Common.Models;
    using ISOOU.Data.Models;
    using ISOOU.Services.Mapping;
    using Microsoft.AspNetCore.Identity;

    public class SystemUserServiceModel : IdentityUser, IMapFrom<SystemUser>, IMapTo<SystemUser>
    { 
        public string FullName { get; set; }

        public string UCN { get; set; }

        public SystemRole UserRole { get; set; }

        public int AdmissionSchoolId { get; set; }

        public ICollection<CandidateServiceModel> Candidates { get; set; }
               
        public ICollection<ParentServiceModel> Parents { get; set; }
               
        public ICollection<QuestionServiceModel> Questions { get; set; }
    }
}
