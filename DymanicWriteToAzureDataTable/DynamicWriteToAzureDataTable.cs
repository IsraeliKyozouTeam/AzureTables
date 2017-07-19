using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure; // Namespace for CloudConfigurationManager
using Microsoft.WindowsAzure.Storage; // Namespace for CloudStorageAccount
using Microsoft.WindowsAzure.Storage.Table; // Namespace for Table storage types

namespace DymanicWriteToAzureStorage
{
    public class DynamicWriteToAzureDataTable
    {
        public CloudTable _table;
        public void WriteData(Dictionary<string,Type> headers, object[,] data ,string storageConnectionString, string tableName = "Table1", string paratitionKey = "Partition1")
        {
            CreateAzureTable(tableName,storageConnectionString);

            int counter = 0;
            TableBatchOperation batchOperetion = new TableBatchOperation();
            for (int j = 0; j < data.GetLength(0); j++)
            {
                Dictionary<string, EntityProperty> dc = new Dictionary<string, EntityProperty>();
                int i = 0;
                foreach (string key in headers.Keys)
                {
                    object o = Activator.CreateInstance(headers[key]);
                    dc.Add(key, ConvertToEntityProperty("", headers[key], data[j, i]));
                    i++;
                }

                DynamicTableEntity te = new DynamicTableEntity(paratitionKey, counter.ToString(), "", dc);
                batchOperetion.Insert(te);
                if ((counter + 1) % 5 == 0)
                {
                    _table.ExecuteBatch(batchOperetion);
                    batchOperetion = new TableBatchOperation();
                }

                counter++;
            }
            if (batchOperetion.Count > 0)
                _table.ExecuteBatch(batchOperetion);




        }

        private void CreateAzureTable(string tableName,string storageConnectionString)
        {
            // Parse the connection string and return a reference to the storage account.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                storageConnectionString);

            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();


            // Retrieve a reference to the table.
            _table = tableClient.GetTableReference(tableName);
            _table.CreateIfNotExists();
            
        }

        private static EntityProperty ConvertToEntityProperty(string key, Type type, object value)
        {
            if (value == null) return new EntityProperty((string)null);
            if (type == typeof(byte[]))
                return new EntityProperty((byte[])value);
            if (type == typeof(bool))
                return new EntityProperty((bool)value);
            if (type == typeof(DateTimeOffset))
                return new EntityProperty((DateTimeOffset)value);
            if (type == typeof(DateTime))
                return new EntityProperty((DateTime)value);
            if (type == typeof(double))
                return new EntityProperty((double)value);
            if (type == typeof(Guid))
                return new EntityProperty((Guid)value);
            if (type == typeof(int))
                return new EntityProperty((int)value);
            if (type == typeof(long))
                return new EntityProperty((long)value);
            if (type == typeof(string))
                return new EntityProperty((string)value);
            if (type == typeof(short))
                return new EntityProperty((short)value);
            if (type == typeof(DBNull))
                return new EntityProperty("");
            throw new Exception("This value type" + type + " for " + key);
            throw new Exception(string.Format("This value type {0} is not supported for {1}", key));
        }
    }
}
