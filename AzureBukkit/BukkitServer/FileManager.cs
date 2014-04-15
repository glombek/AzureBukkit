using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BukkitServer
{
    class FileManager
    {
        public string WorkingDirectory { get { return "WorkingDir"; } }

        private const string TempFilePath = @"C:\Windows\Temp\AzureBukkit";

        internal void Save()
        {
            var workingDirPath = Path.GetFullPath(WorkingDirectory);
            ZipFile.CreateFromDirectory(workingDirPath, Path.Combine(TempFilePath, DateTime.UtcNow.ToString("yyyymmddhhMMss") + ".zip"));

        }
    }
}
