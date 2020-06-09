using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NamedPipeWrapper;
using Newtonsoft.Json;
using VolumeManager.Models;
using VolumeManagerService.Commands;
using VolumeManagerService.Interfaces;

namespace VolumeManager.Services
{
    public class ConnectionManagement : IConnectionManagement
    {
        private const string PIPE_NAME = "pipes";
        private readonly NamedPipeClient<string> _client;

        public delegate void MessageHandler(string message);
        public event MessageHandler Message;

        public ConnectionManagement()
        {
            _client = new NamedPipeClient<string>(PIPE_NAME);
            _client.ServerMessage += OnServerMessage;
            _client.Disconnected += OnDisconnected;
            _client.Start();
        }

        public void SendMessage(Volume message)
        {
            var json = JsonConvert.SerializeObject(message, Formatting.Indented);
            _client.PushMessage(json);
        }

        private void OnServerMessage(NamedPipeConnection<string, string> connection, string message)
        {
            GetMessage(message);
        }

        private void OnDisconnected(NamedPipeConnection<string, string> connection)
        {
            GetMessage(Environment.NewLine + "Disconnected from server");
        }

        public void GetMessage(string message)
        {
            Message?.Invoke(message);
        }
    }
}
