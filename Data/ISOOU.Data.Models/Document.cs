namespace ISOOU.Data.Models
{
    using System;

    public class Document
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime UploadDate { get; set; } = DateTime.UtcNow;

        public int CandidateId { get; set; }

        public Candidate Candidate { get; set; }

        public int SchoolId { get; set; }

        public School School { get; set; }

        public string Url { get; set; }
    }
}
