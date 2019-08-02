using ISOOU.Data.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ISOOU.Web.ViewModels.Schools
{
    public class AdmissionProcedureSchoolViewModel
    {
        public string SchoolName { get; set; }

        public string SchoolDirectorName { get; set; }

        public virtual ICollection<string> SchoolCandidatesUniqueNumbers { get; set; }

        public virtual Dictionary<string, int> FreeSpotsByClasses { get; set; }

        public int Id { get; set; }

        public int Year { get; set; } = DateTime.Now.Year;

        public DateTime? StartApplyDocuments { get; set; }

        public DateTime? EndApplyDocuments { get; set; }

        public DateTime? StartEnrollment { get; set; }

        public DateTime? EndEnrollment { get; set; }

        public string Status { get; set; }

    }
}
