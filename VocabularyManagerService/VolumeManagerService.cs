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
using NamedPipeWrapper;
using NAudio.CoreAudioApi;
using VocabularyManagerService.Services;
using VolumeManagerService.Interfaces;
using VolumeManagerService.Services;

namespace VolumeManagerService
{
    public partial class VocabularyManagerService : ServiceBase
    {
        private readonly ConnectionManagement connection = new ConnectionManagement();
        private readonly SystemVolumeService volumeService = new SystemVolumeService();
        public VocabularyManagerService()
        {
            connection.VolumeData += SetVolume;
            volumeService.VolumeData += VolumeService_VolumeData;
        }

        private void VolumeService_VolumeData(string data)
        {
            connection.Server.PushMessage(data);
        }

        private void SetVolume(string message)
        {
            volumeService.SetVolume(Convert.ToSingle(message));
        }

        protected override void OnStart(string[] args)
        {
            connection.Server.Start();
        }

        protected override void OnStop()
        {
            connection.Server.Stop();
        }


    }


}

