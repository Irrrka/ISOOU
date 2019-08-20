using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace ISOOU.Web.ViewModels.Users
{
    public class UploadDocumentsInputModel
    {
        public int Id { get; set; }

        public IFormFile Application { get; set; }

    }
}
