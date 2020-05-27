using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
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
            this.CanStop = true; 
            this.CanPauseAndContinue = true; 
            this.AutoLog = true;
            server = new IpcServer<string>("test");
            File.Create(Environment.CurrentDirectory + "Log.txt");

        }

        protected override void OnStart(string[] args)
        {
            try
            {
                server.Start<ServerObserver>();
            }
            catch (Exception e)
            {
                StreamWriter sw = new StreamWriter(Environment.CurrentDirectory + "Log.txt", true);
                sw.Write(e.Message);
            }
        }

        protected override void OnStop()
        {
            server.Stop();
        }
    }
}
