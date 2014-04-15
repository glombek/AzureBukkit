using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Threading;
using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Diagnostics;
using Microsoft.WindowsAzure.ServiceRuntime;
using Microsoft.WindowsAzure.Storage;

namespace BukkitServer
{
    public class WorkerRole : RoleEntryPoint
    {
        private CraftBukkitManager CraftBukkitManager { get; set; }

        public override void Run()
        {
            // This is a sample worker implementation. Replace with your logic.
            Trace.TraceInformation("BukkitServer entry point called", "Information");

            while (CraftBukkitManager.Running)
            {
                Thread.Sleep(10000);
                Trace.TraceInformation("Working", "Information");
            }
            Trace.TraceInformation("Stopped", "Information");
        }

        public override bool OnStart()
        {
            // Set the maximum number of concurrent connections 
            ServicePointManager.DefaultConnectionLimit = 12;

            // For information on handling configuration changes
            // see the MSDN topic at http://go.microsoft.com/fwlink/?LinkId=166357.
            //Process p = new Process();
            //p.
            //p.Start();
            FileManager fileManager = new FileManager();

            JavaManager javaManager = new JavaManager(fileManager);
            javaManager.Install();

            CraftBukkitManager = new CraftBukkitManager(javaManager);
            CraftBukkitManager.Start();

            return base.OnStart();
        }

        public override void OnStop()
        {
            CraftBukkitManager.Stop();
            base.OnStop();
        }
    }
}
