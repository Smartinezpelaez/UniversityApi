using Azure.Storage.Blobs;
using System;
using System.IO;

namespace University.Function.Services.Implements
{
    public class BlobService
    {        
        private static BlobServiceClient _blobClient;

        public BlobService(string storageConfigKey)
        {            
            _blobClient = new BlobServiceClient(storageConfigKey);
        }

        public byte[] GetBytes(string container, string name)
        {
            try
            {
                var containerReference = _blobClient.GetBlobContainerClient(container);
                var blobReference = containerReference.GetBlobClient(name);

                byte[] bytes;
                using (var ms = new MemoryStream())
                {
                    blobReference.DownloadTo(ms);
                    bytes = ms.ToArray();
                }
                return bytes;
            }
            catch (Exception ex) { throw ex; }
        }

        public bool Upload(string container, string name, byte[] content)
        {
            try
            {
                var containerReference = _blobClient.GetBlobContainerClient(container);
                var blobReference = containerReference.GetBlobClient(name);
                containerReference.CreateIfNotExists();

                using (var ms = new MemoryStream(content))
                {
                    blobReference.Upload(ms);
                }
                return true;
            }
            catch (Exception ex) { throw ex; }
        }
    }
}
