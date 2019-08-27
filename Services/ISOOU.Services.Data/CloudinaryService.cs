using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using ISOOU.Data.Common.Repositories;
using ISOOU.Data.Models;
using ISOOU.Services.Data.Contracts;
using ISOOU.Services.Mapping;
using ISOOU.Services.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ISOOU.Services.Data
{
    public class CloudinaryService : ICloudinaryService
    {
        private readonly Cloudinary cloudinaryUtility;
        private readonly IRepository<Document> documentsRepository;

        public CloudinaryService(Cloudinary cloudinaryUtility, IRepository<Document> documentsRepository)
        {
            this.cloudinaryUtility = cloudinaryUtility;
            this.documentsRepository = documentsRepository;
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

        public IQueryable<DocumentServiceModel> ViewDocuments(int schoolId)
        {
            var documents = this.documentsRepository.All()
                .Where(s => s.SchoolId == schoolId)
                .To<DocumentServiceModel>();

            return documents;
        }
    }
}
