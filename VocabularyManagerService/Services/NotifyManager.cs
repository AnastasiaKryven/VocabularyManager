using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolumeManagerService.Services;

namespace VocabularyManagerService.Services
{
    public class NotifyManager : INotifyManager
    {
        public delegate void AudioHandler(string data);
        public event AudioHandler AudioNotify;

        public NotifyManager(ISystemVolumeService volumeService)
        {
            volumeService.VolumeData += VolumeService_VolumeData;
        }

        private void VolumeService_VolumeData(string data)
        {
            AudioNotify?.Invoke(data);
        }
    }
}
