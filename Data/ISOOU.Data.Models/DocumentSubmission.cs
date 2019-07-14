namespace ISOOU.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations.Schema;

    public class DocumentSubmission
    {
        public int Id { get; set; }

        public virtual School School { get; set; }

        [ForeignKey("Candidate")]
        public string CandidateId { get; set; }

        public virtual SystemUser Candidate { get; set; }

        public DateTime DateTimeUploaded => DateTime.UtcNow;

        public string PathFile { get; set; }

    }
}
