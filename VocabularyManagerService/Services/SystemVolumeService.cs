using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.CoreAudioApi;

namespace VolumeManagerService.Services
{
    public class SystemVolumeService : ISystemVolumeService
    {
        private readonly MMDeviceEnumerator _deviceEnumerator = new MMDeviceEnumerator();
        private readonly MMDevice _playbackDevice;

        public delegate void VolHandler(string data);
        public event VolHandler VolumeData;

        public SystemVolumeService()
        {
            _playbackDevice = _deviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
            _playbackDevice.AudioEndpointVolume.OnVolumeNotification += AudioEndpointVolume_OnVolumeNotification;
        }

        public int GetVolume()
        {
            return (int)(_playbackDevice.AudioEndpointVolume.MasterVolumeLevelScalar * 100);
        }

        public void SetVolume(int volumeLevel)
        {
            if (volumeLevel < 0 || volumeLevel > 100)
                throw new ArgumentException("Volume must be between 0 and 100!");

            _playbackDevice.AudioEndpointVolume.MasterVolumeLevelScalar = volumeLevel / 100.0f;
        }


        public void AudioEndpointVolume_OnVolumeNotification(AudioVolumeNotificationData data)
        {
            SendToServer((data.MasterVolume * 100).ToString());
        }

        private void SendToServer(string data)
        {
            VolumeData?.Invoke(data);
        }
    }
}
