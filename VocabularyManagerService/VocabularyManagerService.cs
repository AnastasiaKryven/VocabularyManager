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
        //private readonly IpcServer<string> server;

        //ISystemVolumeService systemVolume = new SystemVolumeService();
        //public VocabularyManagerService()
        //{
        //    InitializeComponent();
        //    this.CanStop = true; 
        //    this.CanPauseAndContinue = true; 
        //    this.AutoLog = true;
        //    server = new IpcServer<string>("test");
        //    File.Create(Environment.CurrentDirectory + "Log.txt");

        //}

        //protected override void OnStart(string[] args)
        //{
        //    try
        //    {
        //        server.Start<ServerObserver>();
        //    }
        //    catch (Exception e)
        //    {
        //        StreamWriter sw = new StreamWriter(Environment.CurrentDirectory + "Log.txt", true);
        //        sw.Write(e.Message);
        //    }
        //}

        //protected override void OnContinue()
        //{
        //    SystemVolumeService service = new SystemVolumeService();

        //     service.GetVolume();
        //}

        //protected override void OnStop()
        //{
        //    server.Stop();
        //}
        private readonly NamedPipeServer<string> _server = new NamedPipeServer<string>("test");
        private readonly ISet<string> _clients = new HashSet<string>();
        private readonly MMDeviceEnumerator _deviceEnumerator = new MMDeviceEnumerator();
        private readonly MMDevice _playbackDevice;


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
            };
            _server.Start();

        }

        public void AudioEndpointVolume_OnVolumeNotification(AudioVolumeNotificationData data)
        {
            _server.PushMessage((data.MasterVolume * 100).ToString());
            // MessageBox.Show((data.MasterVolume * 100).ToString());
        }

        protected override void OnStart(string[] args)
        {
           
        }

        protected override void OnStop()
        {
            
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
