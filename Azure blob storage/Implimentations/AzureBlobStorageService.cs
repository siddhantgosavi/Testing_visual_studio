using Azure_blob_storage.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage.Blob;
using Microsoft.WindowsAzure.Storage;
using Microsoft.Azure;

namespace Azure_blob_storage.Services
{
    /// <summary>
    /// Represents the AzureBlobStorageService class. This class cannot be inherited.
    /// </summary>
    public sealed class AzureBlobStorageService : IAzureBlobStorageService
    {
        //Declaring a storage account
        private CloudStorageAccount _storageAccount;

        /// <summary>
        /// Initialises a new instance of <see cref="AzureBlobStorageService"/> class. 
        /// </summary>
        public AzureBlobStorageService()
        {
            // Parse the connection string and return a reference to the storage account.
            _storageAccount = CloudStorageAccount.Parse(
                    CloudConfigurationManager.GetSetting("StorageConnectionStringTest"));
        }

        /// <summary>
        /// This method creates a new blob container in azure storage account.
        /// </summary>
        /// <param name="ContainerNameToBeCreated">The name of the container to be created.</param>
        public void CreateNewContainer(string ContainerNameToBeCreated)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This method is used to delete a blob from a azure storage container.
        /// </summary>
        /// <param name="ContainerName">Name of the container to delete a file from.</param>
        /// <param name="BlobName">Name of the file tp be deleted.</param>
        public void DeleteBlobFromContainer(string ContainerName, string BlobName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This method is used to delete an azure storage container.
        /// </summary>
        /// <param name="ContainerName">Name of the container to be deleted.</param>
        public void DeleteContainerFromStorage(string ContainerNameToBeDeleted)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This method downloads all files from a container to a folder.
        /// </summary>
        /// <param name="ContainerName">Name of the container to download the files from.</param>
        /// <param name="FolderPath">Path of the folder where files are to be downloaded.</param>
        public void DownloadAllBlobsFromContainer(string ContainerName, string FolderPath)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This method downloads a file from blob container.
        /// </summary>
        /// <param name="ContainerName">Name of the container to download the file from.</param>
        /// <param name="Filepath">Path of the loaction where file is to be downloaded.</param>
        public void DownloadBlobFromContainer(string ContainerName, string FilePath)
        {
            throw new NotImplementedException();
        }
        
        /// <summary>
        /// This method displays all containers in the azure storage account.
        /// </summary>
        public void GetAllContainers()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This method inserts a file in blob container.
        /// </summary>
        /// <param name="ContainerName">Name of the container to upload  file into.</param>
        /// <param name="FilePath">Path of the file which is to be uploaded.</param>
        public void InsertBlobInContainer(string ContainerName, string FilePath)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This method inserts all files from a folder in bolb container.
        /// </summary>
        /// <param name="ContainerName">Name of the container to upload the files into.</param>
        /// <param name="Folderpath">Path of the folder from which files are to be uploaded.</param>
        public void InsertBolbsFromFolderToContainer(string ContainerName, string Folderpath)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This method displays all files in a bolb container.
        /// </summary>
        /// <param name="ContainerName">Name of the container to display all files from.</param>
        public void ShowAllBlobsFromContainer(string ContainerName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This method downloads a file from bolb container.
        /// </summary>
        /// <param name="ContainerName">Name of the container to download a file from.</param>
        /// <param name="FilePath">Path of the loaction where files is to be downloaded.</param>
        private void DownloadFromContainer(CloudBlobContainer ContainerName, string FilePath)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// This method uploads a file to bolb container.
        /// </summary>
        /// <param name="ContainerName">Name of the container to upload file into.</param>
        /// <param name="FilePath">Path of the file which is to be uploaded.</param>
        private void UploadToContainer(CloudBlobContainer ContainerName, string FilePath)
        {
            throw new NotImplementedException();
        }
    }
}