using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Queue;

namespace WebJob
{
	// To learn more about Microsoft Azure WebJobs SDK, please see http://go.microsoft.com/fwlink/?LinkID=320976
	class Program
	{
		// Please set the following connection strings in app.config for this WebJob to run:
		// AzureWebJobsDashboard and AzureWebJobsStorage
		private static void Main()
		{
			/*
			var host = new JobHost();
			// The following code ensures that the WebJob will be running continuously
			host.RunAndBlock();
			*/
			CloudStorageAccount storageAccount =
				CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);
			CloudQueueClient queueClient = storageAccount.CreateCloudQueueClient();
			CloudQueue queue = queueClient.GetQueueReference("devcamp");
			queue.CreateIfNotExists();
			CloudQueueMessage message = new CloudQueueMessage("Hello world.");
			queue.AddMessage(message);

			#region Peek

			//CloudQueueMessage peekedMessage = queue.PeekMessage();

			#endregion

			#region Change

			//CloudQueueMessage message = queue.GetMessage();
			//message.SetMessageContent("Updated contents.");
			//queue.UpdateMessage(message, TimeSpan.FromSeconds(0.0), MessageUpdateFields.Content | MessageUpdateFields.Visibility);

			#endregion

			#region de-queue

			//CloudQueueMessage retrievedMessage = queue.GetMessage();
			//queue.DeleteMessage(retrievedMessage);

			#endregion

			#region Message count
			
			//int? cachedMessageCount = queue.ApproximateMessageCount;

			#endregion

			#region Delete queue

			//queue.Delete();

			#endregion
		}
	}
}
