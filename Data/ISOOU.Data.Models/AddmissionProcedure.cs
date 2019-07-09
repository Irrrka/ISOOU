using System;
using System.Collections.Generic;
using System.Text;

namespace ISOOU.Data.Models
{
   public class AddmissionProcedure
    {
        public int Id { get; set; }

        public DateTime StartApplyDocuments { get; set; }

        public DateTime EndApplyDocuments { get; set; }

        public DateTime StartEnrollment { get; set; }

        public DateTime EndEnrollment { get; set; }

        //public School School { get; set; }
    }
}
