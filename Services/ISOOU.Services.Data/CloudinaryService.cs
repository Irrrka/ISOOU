using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using ISOOU.Services.Data.Contracts;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace ISOOU.Services.Data
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary cloudinaryUtility;

        public CloudinaryService(Cloudinary cloudinaryUtility)
        {
            this.cloudinaryUtility = cloudinaryUtility;
        }

        public async Task<string> UploadDocument(IFormFile docFile, string fileName)
        {
            byte[] destintionData;

            using (var ms = new MemoryStream())
            {
                await docFile.CopyToAsync(ms);
                destintionData = ms.ToArray();
            }

            UploadResult uploadResult = null;

            using (var ms = new MemoryStream(destintionData))
            {
                ImageUploadParams uploadParams = new ImageUploadParams
                {
                    Folder = "document_submission",
                    File = new FileDescription(fileName, ms),
                };

                uploadResult = this.cloudinaryUtility.Upload(uploadParams);
            }

            return uploadResult?.SecureUri.AbsoluteUri;

        }
    }
}
