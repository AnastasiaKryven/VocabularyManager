using NamedPipeWrapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using NAudio.CoreAudioApi;
using VocabularyManagerService.Services;

namespace VocabularyManagerService
{
    static class Program
    {
        /// <summary>
        /// Главная точка входа для приложения.
        /// </summary>
        ///
       
        static void Main()
        {
            //var server = new MyServer("test");
            VocabularyManagerService service = new VocabularyManagerService();
            Console.ReadKey();
        }

        //#if DEBUG


            //            var vms = new VocabularyManagerService();

            //            vms.OnDebug();
            //            Console.ReadLine();
            //#endif

            //ServiceBase[] ServicesToRun;
            //ServicesToRun = new ServiceBase[]
            //{
            //    new VocabularyManagerService()
            //};
            //ServiceBase.Run(ServicesToRun);


            //ServiceBase[] ServicesToRun;
            //ServicesToRun = new ServiceBase[]
            //{
            //    new VocabularyManagerService(), 
            //};

            //// In interactive and debug mode ?
            //if (Environment.UserInteractive && System.Diagnostics.Debugger.IsAttached)
            //{
            //    // Simulate the services execution
            //    RunInteractiveServices(ServicesToRun);
            //}
            //else
            //{
            //    // Normal service execution
            //    ServiceBase.Run(ServicesToRun);
            //}

            //server.Start();

            //Console.WriteLine("\nPress return to shutdown server");
            //Console.ReadLine();

            //server.Stop();


        }

        //private static void RunInteractiveServices(ServiceBase[] servicesToRun)
        //{
        //    Console.WriteLine();
        //    Console.WriteLine("Start the services in interactive mode.");
        //    Console.WriteLine();

        //    // Get the method to invoke on each service to start it
        //    MethodInfo onStartMethod = typeof(ServiceBase).GetMethod("OnStart", BindingFlags.Instance | BindingFlags.NonPublic);

        //    // Start services loop
        //    foreach (ServiceBase service in servicesToRun)
        //    {
        //        Console.Write("Starting {0} ... ", service.ServiceName);
        //        onStartMethod.Invoke(service, new object[] { new string[] { } });
        //        Console.WriteLine("Started");
        //    }

        //    // Waiting the end
        //    Console.WriteLine();
        //    Console.WriteLine("Press a key to stop services...");
        //    Console.ReadKey();
        //    MethodInfo onContinueMethod = typeof(ServiceBase).GetMethod("OnContinue", BindingFlags.Instance | BindingFlags.NonPublic);
           
        //    // Stop loop
        //    foreach (ServiceBase service in servicesToRun)
        //    {
        //        Console.Write("Stopping {0} ... ", service.ServiceName);
        //        onContinueMethod.Invoke(service, null);
        //        Console.WriteLine("Stopped");
        //    }

        //    Console.WriteLine();
        //    Console.ReadKey();

        //    Console.WriteLine();

        //    // Get the method to invoke on each service to stop it
        //    MethodInfo onStopMethod = typeof(ServiceBase).GetMethod("OnStop", BindingFlags.Instance | BindingFlags.NonPublic);

        //    // Stop loop
        //    foreach (ServiceBase service in servicesToRun)
        //    {
        //        Console.Write("Stopping {0} ... ", service.ServiceName);
        //        onStopMethod.Invoke(service, null);
        //        Console.WriteLine("Stopped");
        //    }

        //    Console.WriteLine();
        //    Console.WriteLine("All services are stopped.");

        //    // Waiting a key press to not return to VS directly
        //    if (System.Diagnostics.Debugger.IsAttached)
        //    {
        //        Console.WriteLine();
        //        Console.Write("=== Press a key to quit ===");
        //        Console.ReadKey();
        //    }
        //}
    
}
