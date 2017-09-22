using System;
using System.Text.RegularExpressions;
using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Table; // Namespace for Table storage types
using Microsoft.WindowsAzure.Storage.Blob;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Dynamic;

namespace AzureStorageService_POC
{
    public class CloudTableRepository<T> where T : ITableEntity, new()
    {
        private readonly string _storageConnectionSting;
        private CloudStorageAccount cloudStorageConnection;

        public CloudTableRepository(string _storageConnectionSting)
        {
            this._storageConnectionSting = _storageConnectionSting;
            cloudStorageConnection = GetStorageAccountConnection(_storageConnectionSting);
        }



        #region Public Methods

        public virtual async Task<List<T>> GetPartitionAsync(string _tableName, string partitionKey, int takeCount = 1000)
        {
            CloudTable _table = GetTableReference(cloudStorageConnection, _tableName);
            var result = new List<T>();
            var query =
                new TableQuery<T>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal,
                    partitionKey));
            query.TakeCount = takeCount;
            TableContinuationToken tableContinuationToken = null;
            do
            {
                var queryResponse = await _table.ExecuteQuerySegmentedAsync(query, tableContinuationToken);
                tableContinuationToken = queryResponse.ContinuationToken;
                result.AddRange(queryResponse.Results);
            } while (tableContinuationToken != null);
            return result;
        }

        public virtual async Task<List<CloudTable>> GetTableListAsync()
        {
            CloudTableClient tableClient = cloudStorageConnection.CreateCloudTableClient();
            TableContinuationToken tableContinuationToken = null;
            var tables = await tableClient.ListTablesSegmentedAsync(tableContinuationToken);
            return (List<CloudTable>)tables.Results;
        }

        public virtual async Task<TableResult> GetSingleAsync(string _tableName, string partitionKey, string rowKey)
        {
            return await GetSingle(_tableName, partitionKey, rowKey);
        }

        public virtual async Task<T> UpdateAsync(string _tableName, T tableEntityData)
        {
            CloudTable _table = GetTableReference(cloudStorageConnection, _tableName);
            var updateCallistConfig = await GetSingleAsync(_tableName, tableEntityData.PartitionKey, tableEntityData.RowKey);
            if (updateCallistConfig != null)
            {
                var updateOperation = TableOperation.InsertOrMerge(tableEntityData);
                var tableResult = await _table.ExecuteAsync(updateOperation);
                return (T)tableResult.Result;
            }
            return default(T);
        }

        public virtual async Task<T> AddAsync(string _tableName, T tableEntityData)
        {
            CloudTable _table = GetTableReference(cloudStorageConnection, _tableName);
            var retrieveOperation = TableOperation.Insert(tableEntityData);
            var tableResult = await _table.ExecuteAsync(retrieveOperation);
            return (T)tableResult.Result;
        }

        public virtual async Task<bool> CreateTable(string _tableName)
        {
            CloudTable _table = GetTableReference(cloudStorageConnection, _tableName);
            try
            {
                if (!Regex.IsMatch(_tableName, "^[A-Za-z][A-Za-z0-9]{2,62}$"))
                {
                    throw new Exception("Table name is incorect!!");
                }

                // Create the table if it doesn't exist.
                return await _table.CreateIfNotExistsAsync();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public virtual async Task<bool> DeleteTable(string _tableName)
        {
            CloudTable _table = GetTableReference(cloudStorageConnection, _tableName);
            return await _table.DeleteIfExistsAsync();
        }



        #endregion

        #region Private Methods
        private CloudStorageAccount GetStorageAccountConnection(string _StorageAccountConnectionSting)
        {
            return CloudStorageAccount.Parse(_StorageAccountConnectionSting);

        }
        private CloudTable GetTableReference(CloudStorageAccount storageAccount, string tableName)
        {
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Retrieve a reference to the table.
            CloudTable table = tableClient.GetTableReference(tableName);
            return table;
        }

        private async Task<TableResult> GetSingle(string _tableName, string partitionKey, string rowKey)
        {
            CloudTable _table = GetTableReference(cloudStorageConnection, _tableName);
            var retrieveOperation = TableOperation.Retrieve<T>(partitionKey, rowKey);
            var tableResult = await _table.ExecuteAsync(retrieveOperation);
            return tableResult;
        }

        #endregion
    }
    public class TenantEntity : TableEntity
    {

        public TenantEntity(String PartitionKey, string RowKey)
        {
            this.PartitionKey = PartitionKey;
            this.RowKey = RowKey;
        }
        public TenantEntity() { }
        public string Name { get; set; }
        public string Subdomain { get; set; }
        public string TenantConfig { get; set; }

    }

    public class TenantEntity1 : TableEntity
    {

        public TenantEntity1(String PartitionKey, string RowKey)
        {
            this.PartitionKey = PartitionKey;
            this.RowKey = RowKey;
        }
        public TenantEntity1() { }
        public Guid Name { get; set; }
        public int Subdomain { get; set; }
        public string TenantConfig { get; set; }

    }
    class Program
    {

        static void Main(string[] args)
        {
            // Parse the connection string and return a reference to the storage account.
            //DefaultEndpointsProtocol=https;AccountName=apttustenantstore;AccountKey=QGibciDGY8FU3Oa/rqBBoMPgFjimL7j7xgYIZU7t2lZ7KmyT2gmbz1k5zuv+boeK3x5BNbAyN2LBDSdMl8ewaA==
            string StorageConnectionSting = "DefaultEndpointsProtocol=https;AccountName=testpersistentsa;AccountKey=f83gCzLI3D0pD3ZILUpBeZcd2iLRShMg0eXf/RWmd5XeepLFw9x0+sE3XiDxcKU8oA5jaGgdtuQhFjl7CTTWaw==;EndpointSuffix=core.windows.net";
            //string tableName = "Tenant";
            Program p = new Program();
            CloudTableRepository<TenantEntity> master = new CloudTableRepository<TenantEntity>(StorageConnectionSting);

            //TenantEntity tenantentity = new TenantEntity("ApttusGTS", "9");
            //tenantentity.Subdomain = "not clmui";
            //tenantentity.TenantConfig = "test is done";
            //master.UpdateAsync("Tenant", tenantentity);
            Task<List<TenantEntity>> res = master.GetPartitionAsync("Tenant", "ApttusGTS");
            List<TenantEntity> res1 = res.Result;

            Console.ReadLine();

        }
    }
}
