using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using NamedPipeWrapper;
using NAudio.CoreAudioApi;
using VocabularyManagerService.Interfaces;
using VocabularyManagerService.Services;

namespace VocabularyManagerService
{
    public partial class VocabularyManagerService : ServiceBase
    {
        
        private readonly NamedPipeServer<string> _server = new NamedPipeServer<string>("test");
        private readonly ISet<string> _clients = new HashSet<string>();
        private readonly MMDeviceEnumerator _deviceEnumerator = new MMDeviceEnumerator();
        private readonly MMDevice _playbackDevice;
        private readonly SystemVolumeService volumeService = new SystemVolumeService();


        public VocabularyManagerService()
        {
            _playbackDevice = _deviceEnumerator.GetDefaultAudioEndpoint(DataFlow.Render, Role.Multimedia);
            _playbackDevice.AudioEndpointVolume.OnVolumeNotification += AudioEndpointVolume_OnVolumeNotification;
            _server.ClientConnected += OnClientConnected;
            _server.ClientDisconnected += OnClientDisconnected;
            _server.ClientMessage += (client, message) =>
            {
                _server.PushMessage(client.Name + ": " + message);
                Console.WriteLine(client.Name + ": " + message);
                volumeService.SetVolume(Convert.ToSingle(message));
            };

        }

        public void AudioEndpointVolume_OnVolumeNotification(AudioVolumeNotificationData data)
        {
            _server.PushMessage((data.MasterVolume * 100).ToString());
        }

        protected override void OnStart(string[] args)
        {
            _server.Start();
        }

        protected override void OnStop()
        {
            _server.Stop();
        }

        private void OnClientConnected(NamedPipeConnection<string, string> connection)
        {
            _clients.Add(connection.Name);
            Console.WriteLine(connection.Name + " connected!");
            _server.PushMessage(connection.Name + " connected!");
            connection.PushMessage("Welcome!  You are now connected to the server.");
        }

        private void OnClientDisconnected(NamedPipeConnection<string, string> connection)
        {
            _clients.Remove(connection.Name);
            Console.WriteLine(connection.Name + " disconnected!");
            _server.PushMessage(connection.Name + " disconnected!");
        }
    }
}
