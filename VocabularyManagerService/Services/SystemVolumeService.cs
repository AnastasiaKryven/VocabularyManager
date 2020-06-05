using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.CoreAudioApi;
using Newtonsoft.Json;
using VolumeManagerService.Commands;
using VolumeManagerService.Interfaces;
using VolumeManagerService.Services;

namespace VolumeManagerService.Services
{
    public class SystemVolumeService : ISystemVolumeService
    {
        private readonly MMDeviceEnumerator _deviceEnumerator;
        private readonly MMDevice _playbackDevice;
        public event AudioEndpointVolumeNotificationDelegate OnVolumeNotification;

        public SystemVolumeService()
        {
            _deviceEnumerator = new MMDeviceEnumerator();
            _playbackDevice = _deviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
            _playbackDevice.AudioEndpointVolume.OnVolumeNotification += OnVolume;
        }

        public void OnVolume(AudioVolumeNotificationData data)
        {
            this.OnVolumeNotification?.Invoke(data);
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
    }
}
