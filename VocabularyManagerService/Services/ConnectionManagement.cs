using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NamedPipeWrapper;
using NAudio.CoreAudioApi;
using Newtonsoft.Json;
using VolumeManagerService.Commands;
using VolumeManagerService.Interfaces;
using VolumeManagerService.Services;

namespace VolumeManagerService.Services
{
    public class ConnectionManagement : IConnectionManagement
    {
        private const string PIPE_NAME = "pipes";
        private readonly NamedPipeServer<string> _server;
        private readonly ISet<string> _clients;
        private readonly ICommander _commander;

        public ConnectionManagement(ICommander commander, INotifyManager notify)
        {
            this._commander = commander;
            _server = new NamedPipeServer<string>(PIPE_NAME);
            _clients = new HashSet<string>();
            _server.ClientConnected += OnClientConnected;
            _server.ClientDisconnected += OnClientDisconnected;
            _server.ClientMessage += ServerOnClientMessage();
            notify.AudioNotify += Notify_AudioNotify;
        }

        private void Notify_AudioNotify(string data)
        {
            _server.PushMessage(data);
        }

        public ConnectionMessageEventHandler<string, string> ServerOnClientMessage()
        {
            return (client, message) =>
            {
                string messageValue = client.Name + ": " + message;
                _server.PushMessage(messageValue);
                Console.WriteLine(messageValue);
                var json = JsonConvert.DeserializeObject(message);
                _commander.Execute(message);
            };
        }

        public void OnClientConnected(NamedPipeConnection<string, string> connection)
        {
            _clients.Add(connection.Name);
            Console.WriteLine(connection.Name + " connected!");
            _server.PushMessage(connection.Name + " connected!");
            connection.PushMessage("Welcome!  You are now connected to the server...");
        }

        public void OnClientDisconnected(NamedPipeConnection<string, string> connection)
        {
            _clients.Remove(connection.Name);
            Console.WriteLine(connection.Name + " disconnected!");
            _server.PushMessage(connection.Name + " disconnected!");
        }

        public void Start()
        {
            _server.Start();
        }

        public void Stop()
        {
            _server.Stop();
        }

        public void SendMessage(string message)
        {
            _server.PushMessage(message);
        }
    }
}
