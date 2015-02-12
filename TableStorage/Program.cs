using System.Configuration;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;

namespace TableStorage
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			CloudStorageAccount storageAccount =
				CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);

			CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

			CloudTable table = tableClient.GetTableReference("devcamp");
			table.CreateIfNotExists();

			Person person = new Person("Jan", "Pospíšil");
			person.Email = "pospa@pospa.net";

			TableOperation insertOperation = TableOperation.Insert(person);

			table.Execute(insertOperation);

			#region batch

			//TableBatchOperation batchOperation = new TableBatchOperation();

			//batchOperation.Insert(p1);

			//table.ExecuteBatch(batchOperation);

			#endregion

			#region get all

			//CloudTable table = tableClient.GetTableReference("devcamp");

			//TableQuery<Person> query = new TableQuery<Person>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "Smith"));

			//foreach (Person entity in table.ExecuteQuery(query))
			//{
			//	Console.WriteLine("{0}, {1}\t{2}", entity.PartitionKey, entity.RowKey,
			//		entity.Email);
			//}

			#endregion

			#region range

			//CloudTable table = tableClient.GetTableReference("devcamp");

			//// Create the table query.
			//TableQuery<Person> rangeQuery = new TableQuery<Person>().Where(
			//	TableQuery.CombineFilters(
			//		TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, "Smith"),
			//		TableOperators.And,
			//		TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.LessThan, "E")));

			//// Loop through the results, displaying information about the entity.
			//foreach (Person entity in table.ExecuteQuery(rangeQuery))
			//{
			//	Console.WriteLine("{0}, {1}\t{2}", entity.PartitionKey, entity.RowKey,
			//		entity.Email);
			//}

			#endregion
		}
	}

	public class Person : TableEntity
	{
		public Person(string firstName, string lastName)
		{
			PartitionKey = lastName;
			RowKey = firstName;
		}

		public Person()
		{
		}

		public string Email { get; set; }
	}
}