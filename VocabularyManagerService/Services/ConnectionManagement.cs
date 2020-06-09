using NamedPipeWrapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using VolumeManagerService.Commands;

namespace VolumeManagerService.Services
{
    public class ConnectionManagement : IConnectionManagement, ISendMessage
    {
        private const string PIPE_NAME = "pipes";
        private readonly NamedPipeServer<string> _server;
        private readonly ISet<string> _clients;
        private readonly ICommander _commander;

        public ConnectionManagement(ICommander commander)
        {
            this._commander = commander;
            _server = new NamedPipeServer<string>(PIPE_NAME);
            _clients = new HashSet<string>();
            _server.ClientConnected += OnClientConnected;
            _server.ClientDisconnected += OnClientDisconnected;
            _server.ClientMessage += ServerOnClientMessage();
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
