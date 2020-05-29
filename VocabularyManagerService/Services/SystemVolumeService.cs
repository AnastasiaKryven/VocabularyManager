using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NAudio.CoreAudioApi;
using VocabularyManagerService.Interfaces;
using VocabularyManagerService.Models;

namespace VocabularyManagerService.Services
{
    public class SystemVolumeService: ISystemVolumeService
    {
        private readonly MMDeviceEnumerator _deviceEnumerator = new MMDeviceEnumerator();
        private readonly MMDevice _playbackDevice;
        private List<Volume> Volumes = new List<Volume>();

        public SystemVolumeService()
        {
            _playbackDevice = _deviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
        }

        public float GetVolume()
        {
            return (float)(_playbackDevice.AudioEndpointVolume.MasterVolumeLevelScalar * 100);
        }

        public void SetVolume(float volumeLevel)
        {
            if (volumeLevel < 0 || volumeLevel > 100)
                throw new ArgumentException("Volume must be between 0 and 100!");

            _playbackDevice.AudioEndpointVolume.MasterVolumeLevelScalar = volumeLevel / 100.0f;
        }
    }
}
