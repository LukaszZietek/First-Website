using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Services
{
    public class ImageUpload
    {
        static string CreateBlobName()
        {
            return System.Guid.NewGuid().ToString();
        }

        public static string GetFullBlobName(string sizeName, string imageNameWithExtension)
        {
            return $"{sizeName}/{imageNameWithExtension}";
        }

        public string UploadImageAndReturnImageName(HttpPostedFileBase fileBase)
        {
            byte[] image = fileBase.InputStream.ReadFully();
            if (!ImageOptimization.ValidateImage(image))
                return null;

            List<BlobImage> imagesToUpload = GenerateImageMiniatures(image);
            
            try
            {
                UploadMultipleImagesToBlob(imagesToUpload);
            }
            catch
            {
                return null;
            }

            return imagesToUpload.First().ImageName;
        }

        List<BlobImage> GenerateImageMiniatures(byte[] image)
        {
            List<BlobImage> imagesToUpload = new List<BlobImage>();
            string blobName = CreateBlobName();

            foreach (var img in GalleryImages.GalleryDimensionList)
            {
                byte[] imgBytes = ImageOptimization.OptimizeImageFromBytes(img.Width, img.Height, image);

                BlobImage blobImage = new BlobImage()
                {
                    ImgBytes = imgBytes,
                    SizeName = img.SizeName,
                    ImageName = $"{blobName}.{ImageOptimization.GetImageExtension(imgBytes).ToString()}"

                };

                imagesToUpload.Add(blobImage);

            }

            return imagesToUpload;
        }

        void UploadMultipleImagesToBlob(List<BlobImage> images)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            foreach (var img in images)
            {
                CloudBlobContainer container = blobClient.GetContainerReference("zdjecia");
                if (container.CreateIfNotExists())
                {
                    var permissions = container.GetPermissions();
                    permissions.PublicAccess = BlobContainerPublicAccessType.Container;
                    container.SetPermissions(permissions);
                }

                string blobName = GetFullBlobName(img.SizeName, img.ImageName);
                CloudBlockBlob blockBlob = container.GetBlockBlobReference(blobName);
                blockBlob.Properties.ContentType = "image/png";

                blockBlob.UploadFromByteArray(img.ImgBytes,0,img.ImgBytes.Length);

            }

        }

        public void DeleteImageByNameWithMiniatures(string imageNameWithExtension)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("zdjecia");
            foreach (var img in GalleryImages.GalleryDimensionList)
            {
                DeleteImageByName(container, GetFullBlobName(img.SizeName,imageNameWithExtension));

            }

        }

        void DeleteImageByName(CloudBlobContainer container, string fullBlobName)
        {
            CloudBlockBlob blockBob = container.GetBlockBlobReference(fullBlobName);
            blockBob.DeleteIfExists();

        }
    }
}