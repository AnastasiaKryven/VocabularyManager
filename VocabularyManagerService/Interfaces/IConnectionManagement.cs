using NamedPipeWrapper;
using VocabularyManagerService.Models;

namespace VocabularyManagerService.Services
{
    public interface IConnectionManagement
    {
        ConnectionMessageEventHandler<string, string> ServerOnClientMessage();
        void OnClientConnected(NamedPipeConnection<string, string> connection);
        void OnClientDisconnected(NamedPipeConnection<string, string> connection);
        void Start();
        void Stop();
        void SendMessage(string message);
    }
}