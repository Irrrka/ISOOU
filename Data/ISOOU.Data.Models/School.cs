namespace ISOOU.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class School
    {
        private int coefOfYear;

        public School()
        {
            this.Candidates = new HashSet<SystemUser>();
            this.NecessaryDocuments = new HashSet<DocumentSubmission>();
        }

        public int Id { get; set; }

        public string Ref { get; set; }

        public string Name { get; set; }

        public AddressDetails Address { get; set; }

        public SystemUser Director { get; set; }

        public string URLOfSchool { get; set; }

        public string URLOfMap { get; set; }

        public int CandidateId { get; set; }

        public ICollection<SystemUser> Candidates { get; set; }

        public AddmissionProcedure Procedure { get; set; }

        public int NecessaryDocumentId { get; set; }

        public ICollection<DocumentSubmission> NecessaryDocuments { get; set; }

        public int NumberOfClasses { get; set; } = 4;

        public int StudentsPerClass { get; set; } = 18;

        //TODO
        public int FreePlaces => this.CalculateFreePlaces(this.Candidates.FirstOrDefault());

        private int CalculateFreePlaces(SystemUser candidate)
        {
            int result = 0;

            if (candidate.YearOfBirth == (DateTime.Now.Year - 6))
            {
                this.coefOfYear = 2;
                result = this.NumberOfClasses * this.StudentsPerClass * this.coefOfYear;
            }

            if (candidate.YearOfBirth == (DateTime.Now.Year - 7))
            {
                this.coefOfYear = 3;
                result = this.NumberOfClasses * this.StudentsPerClass * this.coefOfYear;
            }

            if (candidate.YearOfBirth == (DateTime.Now.Year - 8))
            {
                this.coefOfYear = 1;
                result = this.NumberOfClasses * this.StudentsPerClass * this.coefOfYear;
            }

            if (candidate.YearOfBirth == (DateTime.Now.Year - 9))
            {
                this.coefOfYear = 1;
                result = this.NumberOfClasses * this.StudentsPerClass * this.coefOfYear;
            }

            if (candidate.YearOfBirth == (DateTime.Now.Year - 10))
            {
                this.coefOfYear = 1;
                result = this.NumberOfClasses * this.StudentsPerClass * this.coefOfYear;
            }

            return result;
        }
    }
}