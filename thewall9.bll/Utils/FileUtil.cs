using Microsoft.Azure;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace thewall9.bll.Utils
{
    public class FileUtil
    {
        public CloudBlobContainer GetBlobContainer(string container)
        {
            // Retrieve storage account from connection string.
            CloudStorageAccount _StorageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // Create the blob client.
            CloudBlobClient _BlobClient = _StorageAccount.CreateCloudBlobClient();

            // Retrieve a reference to a container. 
            CloudBlobContainer _Container = _BlobClient.GetContainerReference(container);
            _Container.CreateIfNotExists();
            // Retrieve reference to a blob named "myblob".
            return _Container;
        }
        public CloudBlockBlob GetBlob(string container, string blobReference)
        {
            CloudBlobContainer _Container=GetBlobContainer(container);
            _Container.SetPermissions(new BlobContainerPermissions
            {
                PublicAccess = BlobContainerPublicAccessType.Blob
            });
            return _Container.GetBlockBlobReference(blobReference);
        }
        public void Delete(string container, string blobReference)
        {
            if (!string.IsNullOrEmpty(blobReference))
                // Create or overwrite the "myblob" blob with contents from a local file.
                GetBlob(container, blobReference).DeleteIfExists();
        }
        public void DeleteFolder(string Container, string Prefix)
        {
            if (!string.IsNullOrEmpty(Prefix))
            {
                var _Container = GetBlobContainer(Container);
                var _Blobs = _Container.ListBlobs(Prefix, true);
                foreach (IListBlobItem blob in _Blobs)
                {
                    _Container.GetBlockBlobReference(((CloudBlockBlob)blob).Name).DeleteIfExists();
                }
            }
        }
        private void UploadFile(System.IO.Stream stream, string container, string blobReference)
        {
            // Create or overwrite the "myblob" blob with contents from a local file.
            var blob = GetBlob(container, blobReference);
            stream.Position = 0;
            blob.UploadFromStream(stream);
        }
        public void UploadImage(System.IO.Stream stream, string container, string blobReference)
        {
            ImageUtil.IsImagen(stream, blobReference);
            UploadFile(stream, container, blobReference);
        }
        public Stream GetFile(string container, string blobReference)
        {
            Stream _F = new MemoryStream();
            GetBlob(container, blobReference).DownloadToStream(_F);
            return _F;
        }
        public bool Exist(string Container, string BlobReference)
        {
            return GetBlob(Container, BlobReference).Exists();
        }
    }
}
