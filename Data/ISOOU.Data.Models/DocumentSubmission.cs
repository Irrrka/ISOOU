namespace ISOOU.Data.Models
{
    using System;

    public class DocumentSubmission
    {
        public int Id { get; set; }

        public School School { get; set; }

        public int CandidateId { get; set; }

        public SystemUser Candidate { get; set; }

        public DateTime DateTimeUploaded { get; set; }

        public string PathFile { get; set; }

    }
}
