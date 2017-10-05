using System;
using Microsoft.WindowsAzure.Storage.Blob;

namespace Azure_blob_storage.Interfaces
{
    /// <summary>
    /// Represents the AzureBlobStorageService interface.
    /// </summary>
    interface IAzureBlobStorageService
    {
        /// <summary>
        /// This method displays all containers in the azure storage account.
        /// </summary>
        void GetAllContainers();

        /// <summary>
        /// This method creates a new blob container in azure storage account.
        /// </summary>
        /// <param name="ContainerNameToBeCreated">The name of the container to be created.</param>
        void CreateNewContainer(String ContainerNameToBeCreated);

        /// <summary>
        /// This method inserts a file in blob container.
        /// </summary>
        /// <param name="ContainerName">Name of the container to upload  file into.</param>
        /// <param name="FilePath">Path of the file which is to be uploaded.</param>
        void InsertBlobInContainer(String ContainerName, String FilePath);

        /// <summary>
        /// This method inserts all files from a folder in bolb container.
        /// </summary>
        /// <param name="ContainerName">Name of the container to upload the files into.</param>
        /// <param name="Folderpath">Path of the folder from which files are to be uploaded.</param>
        void InsertBolbsFromFolderToContainer(String ContainerName, String Folderpath);

        /// <summary>
        /// This method displays all files in a bolb container.
        /// </summary>
        /// <param name="ContainerName">Name of the container to display all files from.</param>
        void ShowAllBlobsFromContainer(String ContainerName);

        /// <summary>
        /// This method downloads a file from blob container.
        /// </summary>
        /// <param name="ContainerName">Name of the container to download the file from.</param>
        /// <param name="Filepath">Path of the loaction where file is to be downloaded.</param>
        void DownloadBlobFromContainer(String ContainerName, String FilePath);

        /// <summary>
        /// This method downloads all files from a container to a folder.
        /// </summary>
        /// <param name="ContainerName">Name of the container to download the files from.</param>
        /// <param name="FolderPath">Path of the folder where files are to be downloaded.</param>>
        void DownloadAllBlobsFromContainer(String ContainerName, String FolderPath);

        /// <summary>
        /// This method is used to delete a blob from a azure storage container.
        /// </summary>
        /// <param name="ContainerName">Name of the container to delete a file from.</param>
        /// <param name="BlobName">Name of the file tp be deleted.</param>
        void DeleteBlobFromContainer(String ContainerName, String BlobName);

        /// <summary>
        /// This method is used to delete an azure storage container.
        /// </summary>
        /// <param name="ContainerName">Name of the container to be deleted.</param>
        void DeleteContainerFromStorage(String ContainerNameToBeDeleted);
    }
}