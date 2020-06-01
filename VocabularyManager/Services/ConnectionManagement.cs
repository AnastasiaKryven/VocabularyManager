using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NamedPipeWrapper;

namespace VocabularyManager.Services
{
    public class ConnectionManagement
    {
        private readonly NamedPipeClient<string> _client = new NamedPipeClient<string>("nana");

        public delegate void MessageHandler(string message);
        public event MessageHandler Message;

        public ConnectionManagement()
        {
            _client.ServerMessage += OnServerMessage;
            _client.Disconnected += OnDisconnected;
            _client.Start();
        }

        public void SendMessage(string message)
        {
            _client.PushMessage(message);
        }

        private void OnServerMessage(NamedPipeConnection<string, string> connection, string message)
        {
            MessageText += Environment.NewLine + "Server" + message;
            SystemValue = message;
        }

        private void OnDisconnected(NamedPipeConnection<string, string> connection)
        {
            MessageText += Environment.NewLine + "Disconnected from server";
        }

        public string MessageText { get; set; }

        public string SystemValue { get; set; }
    }
}
