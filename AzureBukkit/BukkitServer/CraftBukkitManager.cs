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
            //javaManager.FileManager.CreateFileStructure();

            process = new Process();

            process.StartInfo.RedirectStandardInput = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.FileName = javaManager.FullJavaExecutable;
            
            //TODO: Comment Out
            //process.StartInfo.FileName = @"C:\Program Files (x86)\Java\jre6\bin\java.exe";

            var args = new ArgManager();
            args.Add("-Xmx1024M");
            args.Add("-jar", string.Format("\"{0}\"", System.IO.Path.GetFullPath(BukkitPath)));
            args.Add("-o", true);
            args.Add("--nojline");

            args.Add("-b", "{0}/MC/bukkit.yml", javaManager.FileManager.WorkingDirectory);
            args.Add("-c", "{0}/MC/server.properties", javaManager.FileManager.WorkingDirectory);
            args.Add("-P", "{0}/MC/plugins/", javaManager.FileManager.WorkingDirectory);
            args.Add("-W", "{0}/MC/worlds/", javaManager.FileManager.WorkingDirectory);


            process.StartInfo.Arguments = args.ToString();
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

        class ArgManager
        {
            private Dictionary<string, object> Arguments { get; set; }

            public ArgManager()
            {
                Arguments = new Dictionary<string, object>();
            }

            public void Add(string key, object val = null)
            {
                Arguments.Add(key, val);
            }

            public void Add(string key, string format, params object[] values)
            {
                Arguments.Add(key, string.Format(format, values));
            }

            public override string ToString()
            {
                StringBuilder sb = new StringBuilder();
                foreach (var arg in Arguments)
                {
                    if (arg.Value == null)
                    {
                        sb.Append(arg.Key);
                    }
                    else
                    {
                        sb.Append(arg.Key);
                        sb.Append(" ");
                        sb.Append(arg.Value);
                    }
                    sb.Append(" ");
                }
                return sb.ToString();
            }
        }
    }
}