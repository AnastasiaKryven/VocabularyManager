using VocabularyManager.Models;

namespace VocabularyManager.Services
{
    public interface IConnectionManagement
    {
        event ConnectionManagement.MessageHandler Message;
        void GetMessage(string message);
        void SendMessage(Volume message);
    }
}