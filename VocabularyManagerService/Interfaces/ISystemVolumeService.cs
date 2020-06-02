using NAudio.CoreAudioApi;

namespace VolumeManagerService.Services
{
    public interface ISystemVolumeService
    {
        event SystemVolumeService.VolHandler VolumeData;
        float GetVolume();
        void SetVolume(float volumeLevel);
        void AudioEndpointVolume_OnVolumeNotification(AudioVolumeNotificationData data);
    }
}