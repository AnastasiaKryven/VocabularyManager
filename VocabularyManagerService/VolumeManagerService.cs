using System;
using System.ServiceProcess;
using VocabularyManagerService.Services;
using VolumeManagerService.Services;

namespace VolumeManagerService
{
    public partial class VocabularyManagerService : ServiceBase
    {
        private readonly IConnectionManagement connection;
        private readonly ISystemVolumeService volumeService;

        public VocabularyManagerService(ISystemVolumeService volumeService, IConnectionManagement connection)
        {
            this.volumeService = volumeService;
            this.connection = connection;
            connection.VolumeData += SetVolume;
            volumeService.VolumeData += VolumeService_VolumeData;
        }

        private void VolumeService_VolumeData(string data)
        {
            connection.SendMessage(data);
        }

        private void SetVolume(string message)
        {
            volumeService.SetVolume(Convert.ToInt32(message));
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

