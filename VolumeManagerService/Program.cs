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

            container.Register<IConnectionManagement, ConnectionManagement>();
            container.Register<ISendMessage, ConnectionManagement>();
            container.Register<ISystemVolumeService, SystemVolumeService>();

            container.Register<ICommander, Commander>();
            container.Register<ICommand, SetAudioCommand>();
            container.Register<INotifyManager, NotifyManager>();
            container.Register<ICommandFactory, CommandFactory>();


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
