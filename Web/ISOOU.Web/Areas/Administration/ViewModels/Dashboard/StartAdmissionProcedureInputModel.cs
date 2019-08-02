namespace ISOOU.Web.Areas.Administration.ViewModels.Dashboard
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using ISOOU.Data.Models;

    public class StartAdmissionProcedureInputModel
    {
        public DateTime StartApplyDocuments { get; set; }

        public DateTime EndApplyDocuments { get; set; }

        public DateTime StartEnrollment { get; set; }

        public DateTime EndEnrollment { get; set; }

        public AdmissionProcedureStatus Status { get; set; }
    }
}
