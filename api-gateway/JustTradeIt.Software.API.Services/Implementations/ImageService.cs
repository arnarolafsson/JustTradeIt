using System;
using System.IO;
using System.Threading.Tasks;
using Amazon;
using Amazon.S3;
using Amazon.S3.Model;
using JustTradeIt.Software.API.Services.Interfaces;
using Microsoft.AspNetCore.Http;

namespace JustTradeIt.Software.API.Services.Implementations
{
    public class ImageService : IImageService
    {
        private readonly AmazonS3Client s3Client;
        private readonly string _bucketname;
        private readonly string _keyId;
        private readonly string _keySecret;
        
        public ImageService(string bucketname, string keyId, string keySecret)
        {
            _bucketname = bucketname;
            s3Client = new AmazonS3Client(keyId, keySecret, RegionEndpoint.EUWest1);
        }
        public async Task<string> UploadImageToBucket(string email, IFormFile image)
        {
            string result = "";
            string fileName = image.FileName;
            string objectKey = $"Images/{fileName}{email}";
            using (Stream fileToUpload = image.OpenReadStream())
            {
                var putObjectRequest = new PutObjectRequest();
                putObjectRequest.BucketName = _bucketname;
                putObjectRequest.Key = objectKey;
                putObjectRequest.InputStream = fileToUpload;
                putObjectRequest.ContentType = image.ContentType;
                putObjectRequest.CannedACL = S3CannedACL.PublicRead;
                await s3Client.PutObjectAsync(putObjectRequest);
            }

            result = string.Format("http://{0}.s3.amazonaws.com/{1}", _bucketname, objectKey);
            return result;
        }
    }
}