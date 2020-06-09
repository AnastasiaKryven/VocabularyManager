using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Threading.Tasks;

namespace VolumeManagerService
{
    [RunInstaller(true)]
    public partial class Installer2 : System.Configuration.Install.Installer
    {
        readonly ServiceInstaller serviceInstaller;
        readonly ServiceProcessInstaller processInstaller;
        public Installer2()
        {
            InitializeComponent();
            serviceInstaller = new ServiceInstaller();
            processInstaller = new ServiceProcessInstaller
            {
                Account = ServiceAccount.LocalSystem
            };
            serviceInstaller.StartType = ServiceStartMode.Manual;
            serviceInstaller.ServiceName = "VolumeService";
            Installers.Add(processInstaller);
            Installers.Add(serviceInstaller);
        }
    }
}
