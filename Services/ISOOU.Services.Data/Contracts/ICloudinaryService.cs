namespace ISOOU.Services.Data.Contracts
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using ISOOU.Services.Models;
    using Microsoft.AspNetCore.Http;

    public interface ICloudinaryService
    {
        Task<string> UploadDocument(IFormFile docFile, string fileName);

        IQueryable<DocumentServiceModel> ViewDocuments(int schoolId);
    }
}
