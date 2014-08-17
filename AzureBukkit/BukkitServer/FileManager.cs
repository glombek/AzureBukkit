using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BukkitServer
{
    public class FileManager
    {
        public string WorkingDirectory { get { return Path.GetFullPath("WorkingDir"); } }

        private const string TempFilePath = @"C:\Windows\Temp\AzureBukkit";

        internal void Save()
        {
            var path = GetFileName();
            var workingDirPath = Path.GetFullPath(WorkingDirectory + "\\MC");
            ZipFile.CreateFromDirectory(workingDirPath, path);
            SaveToBlob(path);
        }

        private static string GetFileName()
        {
            return Path.Combine(TempFilePath, DateTime.UtcNow.ToString("yyyyMMddHmmss") + ".zip");
        }

        internal void CreateFileStructure()
        {
            if (Directory.Exists(WorkingDirectory))
            {
                Directory.Delete(WorkingDirectory, true);
            }

            while (Directory.Exists(WorkingDirectory))
            {
                Thread.Sleep(1000);
            }

            Directory.CreateDirectory(WorkingDirectory);
            Directory.CreateDirectory(WorkingDirectory +"\\Java");
            Directory.CreateDirectory(WorkingDirectory +"\\MC");

            var path = DownloadBlob();
            if (path != null)
            {
                ZipFile.ExtractToDirectory(path, Path.GetFullPath(WorkingDirectory + "\\MC"));
            }
        }

        internal string DownloadBlob()
        {
            var path = GetFileName();
            var blockBlob = GetBlob();
            if (blockBlob.Exists())
            {
                using (var fileStream = System.IO.File.OpenWrite(path))
                {
                    blockBlob.DownloadToStream(fileStream);
                }
                return path;
            }
            return null;
        }

        internal void SaveToBlob(string filePath)
        {
            CloudBlockBlob blockBlob = GetBlob();

            using (var fileStream = System.IO.File.OpenRead(filePath))
            {
                blockBlob.UploadFromStream(fileStream);
            } 
        }

        private static CloudBlockBlob GetBlob()
        {
            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(
    CloudConfigurationManager.GetSetting("BackupConnectionString"));

            CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();


            CloudBlobContainer container = blobClient.GetContainerReference("azurebukkitbackups");

            container.CreateIfNotExists();

            CloudBlockBlob blockBlob = container.GetBlockBlobReference("latest");
            return blockBlob;
        }
    }
}
