using NAudio.CoreAudioApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolumeManagerService.Services;

namespace VolumeManagerService.Services
{
    public class NotifyManager : INotifyManager
    {
        private ISendMessage _connection;

        public NotifyManager(ISendMessage connection, ISystemVolumeService volumeService)
        {
            this._connection = connection;
            volumeService.OnVolumeNotification += AudioEndpointVolume_OnVolumeNotification;
        }

        public void AudioEndpointVolume_OnVolumeNotification(AudioVolumeNotificationData data)
        {
            var volumeValue = (data.MasterVolume * 100).ToString();

            this.Send(volumeValue);
        }

        private void Send(string message)
        {
            Console.WriteLine(message);
            _connection.SendMessage(message);
        }
    }
}
