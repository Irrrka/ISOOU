namespace ISOOU.Data.Models
{
    using ISOOU.Data.Common.Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class School
    {
        private int coefOfYear;

        public School()
        {
            this.Classes = new HashSet<Class>();
            this.Candidates = new HashSet<Candidates_Schools>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public virtual AddressDetails Address { get; set; }

        public virtual District District { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string DirectorName { get; set; }

        public string URLOfSchool { get; set; }

        public string URLOfMap { get; set; }

        public int CandidateId { get; set; }

        public virtual ICollection<Candidates_Schools> Candidates { get; set; }

        public virtual ICollection<Class> Classes { get; set; }

        public int FreePlaces => this.CalculateFreePlaces();

        private int CalculateFreePlaces()
        {
            int result = 0;
            result = this.Classes.Sum(c => c.NumberOfStudents);

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