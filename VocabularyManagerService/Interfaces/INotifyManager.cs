using NAudio.CoreAudioApi;

namespace VolumeManagerService.Services
{
    public interface INotifyManager
    {
        void AudioEndpointVolume_OnVolumeNotification(AudioVolumeNotificationData data);
    }
}