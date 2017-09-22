using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure; // Namespace for CloudConfigurationManager
using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Blob; // Namespace for Blob storage types
using Microsoft.Azure; //Namespace for CloudConfigurationManager
using Microsoft.Azure.Management.Storage.Models;

namespace Azure_blob_storage
{
    class Program
    {
        // Parse the connection string and return a reference to the storage account.

        private static CloudStorageAccount _storageAccount;
        //static
        //{private static CloudStorageAccount _storageAccount;
        //Console.WriteLine("constructor");
        //    StorageAccount = CloudStorageAccount.Parse(
        //            CloudConfigurationManager.GetSetting("StorageConnectionStringTest"));
        //}

        static void Main(string[] args)
        {
            _storageAccount = CloudStorageAccount.Parse(
                    CloudConfigurationManager.GetSetting("StorageConnectionStringTest"));

            try
            {
                CreateNewContainer("secondontainerfromcode");
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ContainerNameToBeCreated"></param>
        private static void CreateNewContainer(String ContainerNameToBeCreated)
        {
            Boolean Status;

            Console.WriteLine("Creating blob client");
            //Creating a Cloud Blob Client
            CloudBlobClient blobClient = _storageAccount.CreateCloudBlobClient();
            Console.WriteLine("fatla");

            // Retrieve a reference to a container.
            CloudBlobContainer container = blobClient.GetContainerReference(ContainerNameToBeCreated);
            Console.WriteLine(ContainerNameToBeCreated);
            Console.WriteLine("Container reference created \n creating container");

            // Create the container if it doesn't already exist.
            Status = container.CreateIfNotExists();

            Console.WriteLine("container created");

            container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

            Console.WriteLine("public permission given");

            //return Status;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        private static IEnumerable<String> GetAllContainers()
        {


            return new List<String>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="ContainerName"></param>
        /// <returns></returns>
        private static IEnumerable<Byte[]> GetAllBlobsFromContainer(String ContainerName)
        {


            return new List<Byte[]>();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Filepath"></param>
        /// <returns></returns>
        private static Boolean InsertBlobInContainer(String Filepath)
        {


            return true;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Folderpath"></param>
        /// <returns></returns>
        private static Boolean InsertBolbsFromFolderToContainer(String Folderpath)
        {


            return true;
        }
    }
}