using ISOOU.Services.Mapping;
using ISOOU.Services.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ISOOU.Web.ViewModels.Schools
{
    public class EditSchoolViewModel : IMapFrom<SchoolServiceModel>, IMapTo<SchoolServiceModel>
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string DirectorName { get; set; }

        public string Address { get; set; }

        public string DistrictName { get; set; }

        public string Email { get; set; }

        public string PhoneNumber { get; set; }

        public string URLOfSchool { get; set; }

        public string URLOfMap { get; set; }

        public string SchoolClassClassProfile { get; set; }

        public int SchoolClassClassInitialFreeSpots { get; set; }

    }
}
