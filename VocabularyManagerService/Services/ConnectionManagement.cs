using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NamedPipeWrapper;
using NAudio.CoreAudioApi;
using VocabularyManagerService.Commands;
using VocabularyManagerService.Interfaces;
using VolumeManagerService.Services;

namespace VocabularyManagerService.Services
{
    public class ConnectionManagement : IConnectionManagement
    {
        private readonly NamedPipeServer<string> _server = new NamedPipeServer<string>("pipes");
        private readonly ISet<string> _clients = new HashSet<string>();

        public delegate void VolumeHandler(string data);
        public event VolumeHandler VolumeData;
        private ICommand _command;

        public ConnectionManagement()
        {
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

                IncomeValue(message);
                _command = new Commander(message);
                _command.Execute();
                Console.WriteLine("Command");
            };
        }

        public void IncomeValue(string data)
        {
            VolumeData?.Invoke(data);
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
