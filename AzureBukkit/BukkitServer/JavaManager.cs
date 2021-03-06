﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BukkitServer
{
    public class JavaManager
    {
        public JavaManager(FileManager fileManager)
        {
            FileManager = fileManager;
        }

        public FileManager FileManager { get; set; }

        const string InstallerPath = "Resources\\jre-7u51-windows-x64.exe";

        public string JavaDirectory {
            get
            {
                return System.IO.Path.Combine(FileManager.WorkingDirectory, "Java");
            }
        }

        public string FullJavaDirectory
        {
            get
            {
                return System.IO.Path.GetFullPath(JavaDirectory);
            }
        }

        public string FullJavaExecutable
        {
            get
            {
                return System.IO.Path.Combine(FullJavaDirectory, "java.exe");
            }
        }

        public void Install()
        {
            Process installer = new Process();
            installer.StartInfo.UseShellExecute = false;
            installer.StartInfo.FileName = System.IO.Path.GetFullPath(InstallerPath);
            installer.StartInfo.Arguments = string.Format("/s /v\"/qn REBOOT=Suppress INSTALLDIR=\\\"{0}\\\"\"", FullJavaDirectory);
            installer.StartInfo.CreateNoWindow = true;
            installer.Start();
            installer.WaitForExit();
        }
    }
}
