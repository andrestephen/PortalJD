using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace JD.Portal.Web.Util
{
    public class Storage
    {
        private CloudBlobContainer GetCloudBlobContainer()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                    CloudConfigurationManager.GetSetting("portaljdstorage"));
            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
            CloudBlobContainer container = blobClient.GetContainerReference("portaljd-blob-container");
            return container;
        }

        public string SalvarBlob(HttpPostedFileBase arquivo)
        {
            CloudBlobContainer container = this.GetCloudBlobContainer();

            bool sucesso = container.CreateIfNotExists();
            string nomeContainer = container.Name;

            CloudBlockBlob blob = container.GetBlockBlobReference(Guid.NewGuid().ToString() + "_" + arquivo.FileName.Replace(' ', '_'));
            blob.Properties.ContentType = arquivo.ContentType;

            using (var fileStream = arquivo.InputStream)
            {
                blob.UploadFromStream(fileStream);
            }

            return blob.Name;
        }
    }


    
}