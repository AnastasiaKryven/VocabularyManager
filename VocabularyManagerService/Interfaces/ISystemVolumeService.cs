using NAudio.CoreAudioApi;

namespace VolumeManagerService.Services
{
    public interface ISystemVolumeService
    {
        int GetVolume();
        void SetVolume(int volumeLevel);
        void AudioEndpointVolume_OnVolumeNotification(AudioVolumeNotificationData data);
    }
}