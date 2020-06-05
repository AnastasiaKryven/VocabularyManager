using VolumeManager.Models;

namespace VolumeManager.Services
{
    public interface IConnectionManagement
    {
        event ConnectionManagement.MessageHandler Message;
        void GetMessage(string message);
        void SendMessage(Volume message);
    }
}