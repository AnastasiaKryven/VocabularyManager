using System;
using NamedPipeWrapper;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using NAudio.CoreAudioApi;
using VolumeManagerService.Services;
using VolumeManagerService.Services;
using VolumeManagerService.Interfaces;
using VolumeManagerService.Commands;
using SimpleInjector;

namespace VolumeManagerService
{
    static class Program
    {
        static void Main()
        {
            var container = new SimpleInjector.Container();

            container.Register<IConnectionManagement, ConnectionManagement>();
            container.Register<ISystemVolumeService, SystemVolumeService>();

            container.Register<ICommander, Commander>();
            container.Register<ICommand, SetAudioCommand>();
            container.Register<INotifyManager, NotifyManager>();
            container.Register<ICommandFactory, CommandFactory>();

            container.Verify();

            ServiceBase[] ServicesToRun;
            ServicesToRun = new ServiceBase[]
            {
                container.GetInstance<VocabularyManagerService>()
            };
#if DEBUG
            DebugService.RunInteractiveServices(ServicesToRun);
#else            
           
                ServiceBase.Run(ServicesToRun);
#endif
        }
    }
}
