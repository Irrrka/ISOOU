using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace ISOOU.Web.ViewModels.Users
{
    public class CreateDocumentInputModel
    {
        public string Name { get; set; }

        public IFormFile Application { get; set; }

        public DateTime UploadDate { get; set; } = DateTime.UtcNow;

        public int CandidateId { get; set; }

        public int SchoolId { get; set; }

    }
}
