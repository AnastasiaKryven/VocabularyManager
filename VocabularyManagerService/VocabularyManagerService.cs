using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using VocabularyManagerService.Interfaces;
using VocabularyManagerService.Services;

namespace VocabularyManagerService
{
    public partial class VocabularyManagerService : ServiceBase
    {
        private readonly IpcServer<string> server;

        ISystemVolumeService systemVolume = new SystemVolumeService();
        public VocabularyManagerService()
        {
            InitializeComponent();
            server = new IpcServer<string>("test");
        }

        protected override void OnStart(string[] args)
        {

            server.Start<ServerObserver>();
        }

        protected override void OnStop()
        {
            server.Stop();
        }
    }
}
