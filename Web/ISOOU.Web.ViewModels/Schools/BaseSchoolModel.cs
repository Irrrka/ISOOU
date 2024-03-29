﻿namespace ISOOU.Web.ViewModels.Schools
{
    using ISOOU.Data.Models;
    using ISOOU.Services.Mapping;
    using ISOOU.Services.Models;

    public class BaseSchoolModel : IMapFrom<SchoolServiceModel>
    {
        public int Id { get; set; }

        public string Name { get; set; }
    }
}
