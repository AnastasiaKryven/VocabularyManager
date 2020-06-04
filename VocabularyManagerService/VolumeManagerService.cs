using System;
using System.ServiceProcess;
using VocabularyManagerService.Services;
using VolumeManagerService.Services;

namespace VolumeManagerService
{
    public partial class VocabularyManagerService : ServiceBase
    {
        private readonly IConnectionManagement connection;

        public VocabularyManagerService(IConnectionManagement connection)
        {
            this.connection = connection;
        }

        protected override void OnStart(string[] args)
        {
            connection.Start();
        }

        protected override void OnStop()
        {
            connection.Stop();
        }
    }
}

