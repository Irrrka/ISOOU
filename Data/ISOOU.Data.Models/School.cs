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
            this.Classes = new HashSet<Class>();
            this.Candidates = new HashSet<Children_Schools>();
            this.NecessaryDocuments = new HashSet<NecessaryDocuments_Schools>();
        }

        public int Id { get; set; }

        public string Ref { get; set; }

        public string Name { get; set; }

        public int AddressDetailsId { get; set; }

        public AddressDetails AddressDetails { get; set; }

        public SystemUser Director { get; set; }

        public string URLOfSchool { get; set; }

        public string URLOfMap { get; set; }

        public int CandidateId { get; set; }

        public ICollection<Children_Schools> Candidates { get; set; }

        public AdmissionProcedure Procedure { get; set; }

        public AdmissionCriteria AdmissionCriteria { get; set; }

        public int NecessaryDocumentId { get; set; }

        public ICollection<NecessaryDocuments_Schools> NecessaryDocuments { get; set; }

        public ICollection<Class> Classes { get; set; }

        public int FreePlaces => this.CalculateFreePlaces();

        private int CalculateFreePlaces()
        {
            int result = 0;
            result = this.Classes.Sum(c => c.NumberOfCandidates);

            //if (this.Candidates.Where(y=>y.YearOfBirth == DateTime.Now.Year - 6))
            //{
            //    this.coefOfYear = 2;
            //    result = this.NumberOfClasses * this.StudentsPerClass * this.coefOfYear;
            //}

            //if (candidate.YearOfBirth == (DateTime.Now.Year - 7))
            //{
            //    this.coefOfYear = 3;
            //    result = this.NumberOfClasses * this.StudentsPerClass * this.coefOfYear;
            //}

            //if (candidate.YearOfBirth == (DateTime.Now.Year - 8))
            //{
            //    this.coefOfYear = 1;
            //    result = this.NumberOfClasses * this.StudentsPerClass * this.coefOfYear;
            //}

            //if (candidate.YearOfBirth == (DateTime.Now.Year - 9))
            //{
            //    this.coefOfYear = 1;
            //    result = this.NumberOfClasses * this.StudentsPerClass * this.coefOfYear;
            //}

            //if (candidate.YearOfBirth == (DateTime.Now.Year - 10))
            //{
            //    this.coefOfYear = 1;
            //    result = this.NumberOfClasses * this.StudentsPerClass * this.coefOfYear;
            //}

            return result;
        }
    }
}