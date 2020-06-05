using System;
using System.ServiceProcess;
using VocabularyManagerService.Services;
using VolumeManagerService.Services;

namespace VolumeManagerService
{
    public partial class VocabularyManagerService : ServiceBase
    {
        private readonly IConnectionManagement connection;

        public VocabularyManagerService(IConnectionManagement connection, ISystemVolumeService volumeService)
        {
            this.connection = connection;
            volumeService.VolumeData += VolumeService_VolumeData;
        }

        private void VolumeService_VolumeData(string data)
        {
            connection.SendMessage(data);
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

