using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Blob; // Namespace for Blob storage types
using Microsoft.Azure; //Namespace for CloudConfigurationManager

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

            while (true)
            {
                try
                {
                    Console.WriteLine("Press the key of your choice\n\n1.List all Containers\n2.Create a new container\n3.Insert a file in container\n4.Insert files from a folder\n5.Show all files from container\n");
                    int option = Console.ReadKey().KeyChar - 48;
                    switch (option)
                    {
                        case 1:
                            {
                                GetAllContainers();
                                break;
                            }
                        case 2:
                            {
                                Console.Write("\nEnter the name of containor to be created : ");
                                string ContainerNameToBeCreated = Console.ReadLine();

                                CreateNewContainer(ContainerNameToBeCreated);
                                break;
                            }
                        case 3:
                            {
                                Console.Write("\nEnter the Container Name : ");
                                string ContainorNameToUploadTo = Console.ReadLine();

                                Console.Write("\nEnter file path (drag and drop) : ");
                                string PathOfFileTOUpload = Console.ReadLine().Trim('"', ' ', '\\', '/', '@');

                                InsertBlobInContainer(ContainorNameToUploadTo, PathOfFileTOUpload);
                                break;
                            }
                        case 4:
                            {
                                Console.Write("\nEnter the Container Name : ");
                                string ContainorNameToUploadTo = Console.ReadLine();

                                Console.Write("\nEnter path of the folder (drag and drop) : ");
                                string SourceFolder = Console.ReadLine();

                                InsertBolbsFromFolderToContainer(ContainorNameToUploadTo, SourceFolder);
                                break;
                            }
                        case 5:
                            {
                                Console.Write("\nEnter the name of the container : ");
                                string ContaibnerName = Console.ReadLine();
                                ShowAllBlobsFromContainer(ContaibnerName);
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

        }

        /// <summary>
        /// This method displays all containers in the azure storage account.
        /// </summary>
        private static void GetAllContainers()
        {
            //Creating a Cloud Blob Client
            CloudBlobClient blobClient = _storageAccount.CreateCloudBlobClient();

            IEnumerable<CloudBlobContainer> ContainerList = blobClient.ListContainers();
            Console.WriteLine();
            foreach (CloudBlobContainer Container in ContainerList)
            {
                Console.WriteLine(Container.Name);
            }
            Console.WriteLine("\n------------------------------------------------------------------------------------------");
        }

        /// <summary>
        /// This method creates a new blob container in azure storage account.
        /// </summary>
        /// <param name="ContainerNameToBeCreated">The name of the container to be created.</param>
        private static void CreateNewContainer(String ContainerNameToBeCreated)
        {
            //Creating a Cloud Blob Client
            CloudBlobClient blobClient = _storageAccount.CreateCloudBlobClient();

            // Retrieve a reference to a container.
            CloudBlobContainer container = blobClient.GetContainerReference(ContainerNameToBeCreated);

            // Create the container if it doesn't already exist.
            container.CreateIfNotExists();

            container.SetPermissions(new BlobContainerPermissions { PublicAccess = BlobContainerPublicAccessType.Blob });

            Console.WriteLine("\nContainer created with public permission");

            Console.WriteLine("\n------------------------------------------------------------------------------------------");
        }

        /// <summary>
        /// This method inserts a file in blob container.
        /// </summary>
        /// <param name="ContainerName">Name of the container to upload  file into.</param>
        /// <param name="FilePath">Path of the file which is to be uploaded.</param>
        private static void InsertBlobInContainer(String ContainerName, String FilePath)
        {
            CloudBlobClient blobClient = _storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer Container = blobClient.GetContainerReference(ContainerName);

            UploadToContainer(Container, FilePath);

            Console.WriteLine("\nFile uploaded successfully");
            Console.WriteLine("\n------------------------------------------------------------------------------------------");
        }

        /// <summary>
        /// This method inserts all files from a folder in bolb container.
        /// </summary>
        /// <param name="ContainerName">Name of the container to upload the files into.</param>
        /// <param name="Folderpath">Path of the folder from which fies are to be uploaded.</param>
        private static void InsertBolbsFromFolderToContainer(String ContainerName, String Folderpath)
        {
            CloudBlobClient blobClient = _storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer Container = blobClient.GetContainerReference(ContainerName);

            string[] FileEntries = Directory.GetFiles(Folderpath);
            foreach (string FilePath in FileEntries)
            {
                UploadToContainer(Container, FilePath);
            }
            Console.WriteLine("\nAll files uploaded successfully");
            Console.WriteLine("\n------------------------------------------------------------------------------------------");
        }

        /// <summary>
        /// This method displays all files in a bolb container.
        /// </summary>
        /// <param name="ContainerName">Name of the container to display all files from.</param>
        private static void ShowAllBlobsFromContainer(String ContainerName)
        {
            // Create the blob client.
            CloudBlobClient blobClient = _storageAccount.CreateCloudBlobClient();

            // Retrieve reference to a previously created container.
            CloudBlobContainer container = blobClient.GetContainerReference(ContainerName);

            // Loop over items within the container and output the length and URI.
            foreach (IListBlobItem item in container.ListBlobs(null, false))
            {
                if (item.GetType() == typeof(CloudBlockBlob))
                {
                    CloudBlockBlob blob = (CloudBlockBlob)item;

                    Console.WriteLine("Block blob of length {0}: {1}", blob.Properties.Length, blob.Uri);
                }
                else if (item.GetType() == typeof(CloudPageBlob))
                {
                    CloudPageBlob pageBlob = (CloudPageBlob)item;

                    Console.WriteLine("Page blob of length {0}: {1}", pageBlob.Properties.Length, pageBlob.Uri);

                }
                else if (item.GetType() == typeof(CloudBlobDirectory))
                {
                    CloudBlobDirectory directory = (CloudBlobDirectory)item;

                    Console.WriteLine("Directory: {0}", directory.Uri);
                }
            }
            Console.WriteLine("\n------------------------------------------------------------------------------------------");
        }

        /// <summary>
        /// This method uploads a file to bolb container.
        /// </summary>
        /// <param name="ContainerName">Name of the container to upload  file into.</param>
        /// <param name="FilePath">Path of the file which is to be uploaded.</param>
        private static void UploadToContainer(CloudBlobContainer ContainerName, String FilePath)
        {
            String File = FilePath.Split('\\').Last();
            // Retrieve reference to a blob named "myblob".
            CloudBlockBlob blockBlob = ContainerName.GetBlockBlobReference(File);

            // Create or overwrite the "myblob" blob with contents from a local file.
            using (var fileStream = System.IO.File.OpenRead(FilePath))
            {
                blockBlob.UploadFromStream(fileStream);
            }
        }
    }
}