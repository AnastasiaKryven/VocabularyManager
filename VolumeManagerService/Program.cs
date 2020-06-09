using SimpleInjector;
using System.ServiceProcess;
using VolumeManagerService.Commands;
using VolumeManagerService.Interfaces;
using VolumeManagerService.Services;

namespace VolumeManagerService
{
    static class Program
    {
        static void Main()
        {
            var container = new SimpleInjector.Container();

            container.Register<INotifyManager, NotifyManager>();
            container.Register<ICommandFactory, CommandFactory>();
            container.Register<ICommander, Commander>();
            container.Register<ICommand, SetAudioCommand>();
            container.Register<ISystemVolumeService, SystemVolumeService>();

            //container.RegisterSingleton<ConnectionManagement>();
            //container.RegisterSingleton<IConnectionManagement>(() => container.GetInstance<ConnectionManagement>());
            //container.RegisterSingleton<ISendMessage>(() => container.GetInstance<ConnectionManagement>());
           

            container.Register<IConnectionManagement>(() => ConnectionManagement.GetInstance(container.GetInstance<ICommander>()));

            container.Register<ISendMessage>(() => ConnectionManagement.GetInstance(container.GetInstance<ICommander>()));

            container.Verify();

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                container.GetInstance<VolumeManagerService>()
            };
#if DEBUG
            DebugService.RunInteractiveServices(ServicesToRun);
#else            
           
                ServiceBase.Run(ServicesToRun);
#endif
        }
    }
}
