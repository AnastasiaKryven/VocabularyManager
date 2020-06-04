using NamedPipeWrapper;

namespace VocabularyManagerService.Services
{
    public interface IConnectionManagement
    {
        event ConnectionManagement.VolumeHandler VolumeData;
        ConnectionMessageEventHandler<string, string> ServerOnClientMessage();
        void OnClientConnected(NamedPipeConnection<string, string> connection);
        void OnClientDisconnected(NamedPipeConnection<string, string> connection);
        void Start();
        void Stop();
        void SendMessage(string message);
    }
}