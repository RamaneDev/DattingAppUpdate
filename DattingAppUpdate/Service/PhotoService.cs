using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using DattingAppUpdate.Helpers;
using DattingAppUpdate.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System.Threading.Tasks;

namespace DattingAppUpdate.Service
{
    public class PhotoService : IPhotoService
    {
        private Cloudinary _cloudinary;
        public PhotoService(IOptions<CloudinaryOptions> cfg)
        {
            var acc = new Account(
                cfg.Value.Cloud_name,
                cfg.Value.Api_key,
                cfg.Value.Api_secret
                );

            _cloudinary = new Cloudinary( acc );
            
        }


        public async Task<ImageUploadResult> AddPhotoAsync(IFormFile file)
        {
            var uploadResult = new ImageUploadResult();

            if (file.Length > 0)
            {
                using var stream = file.OpenReadStream();
                var uploadParams = new ImageUploadParams
                {
                    File = new FileDescription(file.FileName, stream),
                    Transformation = new Transformation().Height(500).Width(500).Crop("fill").Gravity("face")
                };
                uploadResult = await _cloudinary.UploadAsync(uploadParams);
            }

            return uploadResult;
        }

        public async Task<DeletionResult> DeletePhotoAsync(string publicId)
        {
            var deleteParams = new DeletionParams(publicId);

            var result = await _cloudinary.DestroyAsync(deleteParams);

            return result;
        }
    }
}
