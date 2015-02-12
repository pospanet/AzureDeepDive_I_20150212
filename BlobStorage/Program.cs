using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;

namespace BlobStorage
{
	internal class Program
	{
		private static void Main(string[] args)
		{
			CloudStorageAccount storageAccount =
				CloudStorageAccount.Parse(ConfigurationManager.AppSettings["StorageConnectionString"]);
			CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
			CloudBlobContainer container = blobClient.GetContainerReference("devcamp");

			container.CreateIfNotExists();
			container.SetPermissions(
				new BlobContainerPermissions
				{
					PublicAccess =
						BlobContainerPublicAccessType.Blob
				});
			CloudBlockBlob blockBlob = container.GetBlockBlobReference("devcamp");

			// Create or overwrite the "myblob" blob with contents from a local file.
			using (FileStream fileStream = System.IO.File.OpenRead(@"data.txt"))
			{
				blockBlob.UploadFromStream(fileStream);
			}

			#region List blobs

			//CloudBlobContainer container = blobClient.GetContainerReference("photos");

			//foreach (IListBlobItem item in container.ListBlobs(null, false))
			//{
			//	if (item.GetType() == typeof (CloudBlockBlob))
			//	{
			//		CloudBlockBlob blob = (CloudBlockBlob) item;

			//		Console.WriteLine("Block blob of length {0}: {1}", blob.Properties.Length, blob.Uri);

			//	}
			//	else if (item.GetType() == typeof (CloudPageBlob))
			//	{
			//		CloudPageBlob pageBlob = (CloudPageBlob) item;

			//		Console.WriteLine("Page blob of length {0}: {1}", pageBlob.Properties.Length, pageBlob.Uri);

			//	}
			//	else if (item.GetType() == typeof (CloudBlobDirectory))
			//	{
			//		CloudBlobDirectory directory = (CloudBlobDirectory) item;

			//		Console.WriteLine("Directory: {0}", directory.Uri);
			//	}

			#endregion

			#region download blob

			//CloudBlobContainer container = blobClient.GetContainerReference("mycontainer");

			//CloudBlockBlob blockBlob = container.GetBlockBlobReference("photo1.jpg");

			//using (var fileStream = System.IO.File.OpenWrite(@"path\myfile"))
			//{
			//	blockBlob.DownloadToStream(fileStream);
			//}

			#endregion

			#region delete blob

			//CloudBlobContainer container = blobClient.GetContainerReference("mycontainer");

			//CloudBlockBlob blockBlob = container.GetBlockBlobReference("myblob.txt");

			//blockBlob.Delete();

			#endregion
		}
	}
}