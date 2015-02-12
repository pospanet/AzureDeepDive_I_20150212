using System;
using System.Net;
using System.Threading;
using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.ServiceRuntime;

namespace WorkerRole
{
	public class WorkerRole : RoleEntryPoint
	{
		// The name of your queue
		private const string QueueName = "Queue";

		// QueueClient is thread-safe. Recommended that you cache 
		// rather than recreating it on every request
		private QueueClient Client;
		private QueueClient DeadLetterClient;

		public override void Run()
		{
			while (true)
			{
				BrokeredMessage message = Client.Receive(TimeSpan.FromSeconds(5));
				//IEnumerable<BrokeredMessage> messages = Client.ReceiveBatch(100, TimeSpan.FromSeconds(10));
				if (message != null)
				{
					switch (message.Properties["type"].ToString())
					{
						case "string":
							string str = message.GetBody<string>();
							//ToDo
							break;
						case "CustomMessage":
							CustomMessage customMessage = message.GetBody<CustomMessage>();
							//ToDo
							break;
					}
					try
					{
						message.Complete();
					}
					catch (Exception ex)
					{
						//See https://msdn.microsoft.com/query/dev12.query?appId=Dev12IDEF1&l=EN-US&k=k(Microsoft.ServiceBus.Messaging.BrokeredMessage.Complete);k(TargetFrameworkMoniker-.NETFramework,Version%3Dv4.5);k(DevLang-csharp)&rd=true
					}
				}
				Thread.Sleep(TimeSpan.FromMilliseconds(100));

				#region send message

				//BrokeredMessage brokeredMessage = new BrokeredMessage(new CustomMessage());
				//brokeredMessage.Properties.Add("type", "CustomMessage");
				//Client.Send(brokeredMessage);

				#endregion
			}
		}

		public override bool OnStart()
		{
			// Set the maximum number of concurrent connections 
			ServicePointManager.DefaultConnectionLimit = 12;

			// Create the queue if it does not exist already
			string connectionString = CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");
			//NamespaceManager namespaceManager = NamespaceManager.CreateFromConnectionString(connectionString);
			MessagingFactory factory = MessagingFactory.CreateFromConnectionString(connectionString);
			Client = factory.CreateQueueClient(QueueName);

			//DeadLetter will be removed from queue right after Receive
			DeadLetterClient = factory.CreateQueueClient(QueueClient.FormatDeadLetterPath(Client.Path),
				ReceiveMode.ReceiveAndDelete);
			return base.OnStart();
		}

		public override void OnStop()
		{
			// Close the connection to Service Bus Queue
			Client.Close();
			DeadLetterClient.Close();
			base.OnStop();
		}
	}
}