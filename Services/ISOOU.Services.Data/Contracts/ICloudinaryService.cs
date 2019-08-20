using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ISOOU.Services.Data.Contracts
{
    public interface ICloudinaryService
    {
        Task<string> UploadDocument(IFormFile docFile, string fileName);
    }
}
