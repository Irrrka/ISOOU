using System;
using System.Collections.Generic;
using System.Text;

namespace ISOOU.Data.Models
{
    public class DocumentSubmission
    {
        public int Id { get; set; }

        public School School { get; set; }

        public SystemUser Candidate { get; set; }

        public DateTime DateTimeUploaded { get; set; }

        public string PathFile { get; set; }

    }
}
