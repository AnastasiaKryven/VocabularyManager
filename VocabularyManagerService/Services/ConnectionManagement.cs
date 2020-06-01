using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NamedPipeWrapper;
using NAudio.CoreAudioApi;
using VolumeManagerService.Services;

namespace VocabularyManagerService.Services
{
    public class ConnectionManagement
    {
        public readonly NamedPipeServer<string> Server = new NamedPipeServer<string>("nana");
        private readonly ISet<string> _clients = new HashSet<string>();

        public delegate void VolumeHandler(string data);
        public event VolumeHandler VolumeData;

        public ConnectionManagement()
        {
            Server.ClientConnected += OnClientConnected;
            Server.ClientDisconnected += OnClientDisconnected;
            Server.ClientMessage += ServerOnClientMessage();
        }

        private ConnectionMessageEventHandler<string, string> ServerOnClientMessage()
        {
            return (client, message) =>
            {
                string messageValue = client.Name + ": " + message;
                Server.PushMessage(messageValue);
                Console.WriteLine(messageValue);
                SetVolume(message);
            };
        }

        private void SetVolume(string data)
        {
            VolumeData?.Invoke(data);
        }

        private void OnClientConnected(NamedPipeConnection<string, string> connection)
        {
            _clients.Add(connection.Name);
            Console.WriteLine(connection.Name + " connected!");
            Server.PushMessage(connection.Name + " connected!");
            connection.PushMessage("Welcome!  You are now connected to the server.");
        }

        private void OnClientDisconnected(NamedPipeConnection<string, string> connection)
        {
            _clients.Remove(connection.Name);
            Console.WriteLine(connection.Name + " disconnected!");
            Server.PushMessage(connection.Name + " disconnected!");
        }
    }
}
