using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Auth;
using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace amittestRG
{
    public class WriteToFile
    {
        
        System.Data.DataTable data = new System.Data.DataTable();


        public void readData()
        {
            dao d = new dao();
            data = d.GetData();
        }

        public void writeData()
        {
            readData();
            // Retrieve the storage account from the connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();
            

            // Retrieve a reference to the table.
            CloudTable table = tableClient.GetTableReference("data");
            table.CreateIfNotExists();
            List<TableEntity1> data1 = new List<TableEntity1>();
            //for (int i = 0; i < data.Rows.Count; i++)
            foreach(System.Data.DataRow row in data.Rows)
            {
                data1.Add(new TableEntity1(Convert.ToInt32(row[0]), Convert.ToInt16(row[1]), Convert.ToInt32(row[2]), row[3].ToString(), row[4].ToString(), Convert.ToInt32(row[5]), row[6].ToString(), row[7].ToString(), row[8].ToString(), row[9].ToString(), row[10].ToString(), row[11].ToString(), row[12].ToString(), Convert.ToInt16(row[13])));
            }
            int counter = 0;
            TableBatchOperation batchOperetion = new TableBatchOperation();
            foreach (TableEntity1 item in data1)
            {
                
                item.PartitionKey = "amitKey1";
                item.RowKey = counter.ToString();
                // Create the TableOperation object that inserts the customer entity.
                //TableOperation insertOperation = TableOperation.Insert(item);
                // Execute the insert operation.
                //table.Execute(insertOperation);
                batchOperetion.Insert(item);
                if ((counter +1) % 100 == 0)
                {
                    table.ExecuteBatch(batchOperetion);
                    batchOperetion = new TableBatchOperation();
                }

                counter++;
            }
            if(batchOperetion.Count > 0)
                table.ExecuteBatch(batchOperetion);
            



        }
        public void ReadFromTable(string rowKey)
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
            CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Create the CloudTable object that represents the "people" table.
            CloudTable table = tableClient.GetTableReference("data");

            // Create a retrieve operation that takes a customer entity.
            TableOperation retrieveOperation = TableOperation.Retrieve<TableEntity1>("amitKey1", rowKey);

            // Execute the retrieve operation.
            TableResult retrievedResult = table.Execute(retrieveOperation);
            Console.WriteLine(((TableEntity1)retrievedResult.Result).Id);
            
        }
        public void DeleteLogsNotFromLast30Days()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
            CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            // Create the CloudTable object that represents the "people" table.
            CloudTable table = tableClient.GetTableReference("data");

            TableQuery<TableEntity1> rangeQuery = new TableQuery<TableEntity1>()
                .Where(TableQuery.GenerateFilterConditionForDate("Timestamp", QueryComparisons.LessThan
                , DateTime.Now.AddDays(-30)));

            try
            {

                TableOperation deleteOperation;
                // Loop through the results, displaying information about the entity.
                foreach (TableEntity1 entity in table.ExecuteQuery(rangeQuery))
                {
                    deleteOperation = TableOperation.Delete(entity);

                    table.Execute(deleteOperation);
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }
        public void writeData1()
        {
            readData();
            // Retrieve the storage account from the connection string.
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
                CloudConfigurationManager.GetSetting("StorageConnectionString"));

            // Create the table client.
            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();


            // Retrieve a reference to the table.
            CloudTable table = tableClient.GetTableReference("data");
            table.CreateIfNotExists();
            

            int counter = 0;
            TableBatchOperation batchOperetion = new TableBatchOperation();
            foreach (System.Data.DataRow item in data.Rows)
            {
                Dictionary<string, EntityProperty> dc = new Dictionary<string, EntityProperty>();
                var c = data.Columns;
                for (int i = 0; i < item.ItemArray.Length; i++)
                {
                    
                    dc.Add(c[i].ColumnName, ConvertToEntityProperty("",item[i]));
                }
                DynamicTableEntity te = new DynamicTableEntity("amitkey1",counter.ToString(),"",dc);
                batchOperetion.Insert(te);
                if ((counter + 1) % 5 == 0)
                {
                    table.ExecuteBatch(batchOperetion);
                    batchOperetion = new TableBatchOperation();
                }

                counter++;
            }
            if (batchOperetion.Count > 0)
                table.ExecuteBatch(batchOperetion);




        }
        private EntityProperty ConvertToEntityProperty(string key, object value)
        {
            if (value == null) return new EntityProperty((string)null);
            if (value.GetType() == typeof(byte[]))
                return new EntityProperty((byte[])value);
            if (value.GetType() == typeof(bool))
                return new EntityProperty((bool)value);
            if (value.GetType() == typeof(DateTimeOffset))
                return new EntityProperty((DateTimeOffset)value);
            if (value.GetType() == typeof(DateTime))
                return new EntityProperty((DateTime)value);
            if (value.GetType() == typeof(double))
                return new EntityProperty((double)value);
            if (value.GetType() == typeof(Guid))
                return new EntityProperty((Guid)value);
            if (value.GetType() == typeof(int))
                return new EntityProperty((int)value);
            if (value.GetType() == typeof(long))
                return new EntityProperty((long)value);
            if (value.GetType() == typeof(string))
                return new EntityProperty((string)value);
            if (value.GetType() == typeof(short))
                return new EntityProperty((short)value);
            if (value.GetType() == typeof(DBNull))
                return new EntityProperty("");
            throw new Exception("This value type" + value.GetType() + " for " + key);
            throw new Exception(string.Format("This value type {0} is not supported for {1}", key));
        }

    }
}
