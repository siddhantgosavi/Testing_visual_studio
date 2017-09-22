using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure; // Namespace for CloudConfigurationManager
using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Blob; // Namespace for Blob storage types
using Microsoft.Azure; //Namespace for CloudConfigurationManager

namespace Azure_blob_storage
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                Console.WriteLine("Parsing storage account key");
                // Parse the connection string and return a reference to the storage account.
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                    CloudConfigurationManager.GetSetting("StorageConnectionStringSid"));

                Console.WriteLine("Creating blob client");
                //Creating a Cloud Blob Client
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();

                //This is the url to access blob in browser.
                //https://sidstoragetrial1.blob.core.windows.net/first-container
                //In .Net Code u ll require a connection string

                // Retrieve a reference to a container.
                CloudBlobContainer container = blobClient.GetContainerReference("testcontainerfromcode1");

                Console.WriteLine("Container reference created \n creating container");

                // Create the container if it doesn't already exist.
                container.CreateIfNotExists();
                
                Console.WriteLine("created container");



                container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

                Console.WriteLine("public permission given");

            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}