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

            FileManager fileManager = null;


            try
            {
                fileManager = new FileManager();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed loading FileManager.", ex);
            }

            try
            {
                fileManager.CreateFileStructure();

            }
            catch (Exception ex)
            {
                throw new Exception("Failed creating file structure.", ex);
            }

            JavaManager javaManager = null;

            try
            {
                javaManager = new JavaManager(fileManager);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed loading JavaManager.", ex);
            }
            try
            {
                javaManager.Install();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed installing Java.", ex);
            }

            try
            {
                CraftBukkitManager = new CraftBukkitManager(javaManager);
            }
            catch (Exception ex)
            {
                throw new Exception("Failed loading CraftBukkitManager.", ex);
            }

            try
            {
                CraftBukkitManager.Start();
            }
            catch (Exception ex)
            {
                throw new Exception("Failed starting CraftBukkit.", ex);
            }

            return base.OnStart();
        }

        public override void OnStop()
        {
            CraftBukkitManager.Stop();
            base.OnStop();
        }
    }
}
