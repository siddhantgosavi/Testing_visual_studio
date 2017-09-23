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
        //Declaring a storage account
        private static CloudStorageAccount _storageAccount;        

        static void Main(string[] args)
        {
            // Parse the connection string and return a reference to the storage account.
            _storageAccount = CloudStorageAccount.Parse(
                    CloudConfigurationManager.GetSetting("StorageConnectionStringTest"));

            try
            {
                while (true)
                {
                    Console.WriteLine("1.List all Containers\n2.Create a new container\n3.Insert a file in container\n4.Insert files from a folder\n5.Get all files from container\n");
                    int option = Console.Read() - 48;
                    switch (option)
                    {
                        case 1:
                            {
                                IEnumerable<string> AllFiles = GetAllContainers();
                                break;
                            }
                        case 2:
                            {
                                CreateNewContainer("second");
                                break;
                            }
                        case 3:
                            {
                                string ContainerNameToUploadTo, PathOfFileTOUpload;

                                Console.WriteLine("\nEnter the Container Name : ");
                                ContainerNameToUploadTo = Console.ReadLine();
                                ContainerNameToUploadTo = Console.ReadLine();

                                Console.WriteLine("\nEnter file path : ");
                                PathOfFileTOUpload = Console.ReadLine().Trim('"',' ','\\','/','@');
                                                                
                                InsertBlobInContainer(ContainerNameToUploadTo, PathOfFileTOUpload);
                                break;
                            }
                        case 4:
                            {
                                InsertBolbsFromFolderToContainer("Insert a folder path here");
                                break;
                            }
                        case 5:
                            {
                                IEnumerable<Byte[]> AllBolbs = GetAllBlobsFromContainer("Put name of the container here");
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }                    
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
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
        /// <param name="ContainerNameToBeCreated"></param>
        private static void CreateNewContainer(String ContainerNameToBeCreated)
        { 
            Console.WriteLine("Creating blob client");
            //Creating a Cloud Blob Client
            CloudBlobClient blobClient = _storageAccount.CreateCloudBlobClient();

            // Retrieve a reference to a container.
            CloudBlobContainer container = blobClient.GetContainerReference(ContainerNameToBeCreated);
            Console.WriteLine(ContainerNameToBeCreated);
            Console.WriteLine("Container reference created \n creating container");

            // Create the container if it doesn't already exist.
            container.CreateIfNotExists();

            Console.WriteLine("container created");

            container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

            Console.WriteLine("public permission given");           
        }
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="Filepath"></param>
        /// <returns></returns>
        private static void InsertBlobInContainer(String ContainerName, String Filepath)
        {
            CloudBlobClient blobClient = _storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference(ContainerName);

            String File = Filepath.Split('\\').Last();
            // Retrieve reference to a blob named "myblob".
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(File);

            // Create or overwrite the "myblob" blob with contents from a local file.
            using (var fileStream = System.IO.File.OpenRead(Filepath))
            {
                blockBlob.UploadFromStream(fileStream);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="Folderpath"></param>
        /// <returns></returns>
        private static void InsertBolbsFromFolderToContainer(String Folderpath)
        {
            
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

    }
}