using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BukkitServer
{
    class CraftBukkitManager
    {
        public CraftBukkitManager(JavaManager javaManager)
        {
            this.javaManager = javaManager;
        }
        const string BukkitPath = "Resources\\craftbukkit-1.6.4-R2.0.jar";

        private JavaManager javaManager;

        private Process process;

        public bool Running
        {
            get
            {
                return (process != null && !process.HasExited);
            }
        }

        public void Start()
        {
            process = new Process();

            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.FileName = javaManager.FullJavaExecutable;
            
            process.StartInfo.FileName = @"C:\Program Files (x86)\Java\jre6\bin\java.exe";
            
            process.StartInfo.Arguments = string.Format("-Xmx1024M -jar \"{0}\" -o true --nojline", System.IO.Path.GetFullPath(BukkitPath));
            //process.StartInfo.CreateNoWindow = true;
            process.Start();
        }

        private void Save()
        {
            javaManager.FileManager.Save();
        }

        public void Stop()
        {
            if (process != null && !process.HasExited)
            {
                process.StandardInput.WriteLine("stop");
                process.WaitForExit();
            }

            Save();
        }
    }
}