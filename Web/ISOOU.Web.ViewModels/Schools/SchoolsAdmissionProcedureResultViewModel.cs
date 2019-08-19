namespace ISOOU.Web.ViewModels.Schools
{
    using System.Collections.Generic;

    using ISOOU.Services.Mapping;
    using ISOOU.Services.Models;

    public class SchoolsAdmissionProcedureResultViewModel : IMapFrom<AdmissionProcedureServiceModel>
    {
        public string Status { get; set; }
    }
}
