using System;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using NamedPipeWrapper;
using VocabularyManager.Models;

namespace VocabularyManager.ViewModel
{

    public class MainViewModel : ViewModelBase
    {
        private ObservableCollection<VolumeModel> volumeModels;
        private readonly NamedPipeClient<string> _client = new NamedPipeClient<string>("test");
        public ObservableCollection<VolumeModel> Volume
        {
            get { return volumeModels; }
            set
            {
                volumeModels = value;
                RaisePropertyChanged("Volumes");
            }
        }

        private VolumeModel volumeModel;

        public VolumeModel VolumeModel
        {
            get { return volumeModel; }
            set
            {
                volumeModel = value;
                RaisePropertyChanged("Volume");
            }
        }

        private float currentValue;

        public float CurrentValue
        {
            get { return currentValue; }
            set
            {
                currentValue = value;
                RaisePropertyChanged("CurrentValue");
            }
        }

        public RelayCommand ApplyCommand { get; set; }

        public RelayCommand GetVolumeCommand { get; set; }

        public MainViewModel()
        {
            //_client.ServerMessage += OnServerMessage;
            //_client.Disconnected += OnDisconnected;
            //_client.Start();

            VolumeModel = new VolumeModel();
            Volume = new ObservableCollection<VolumeModel>();
            ApplyCommand = new RelayCommand(Apply);
            GetVolumeCommand = new RelayCommand(GetVolume);
        }

        void Apply()
        {
            _client.PushMessage(currentValue.ToString());
        }

        public string SystemValue { get; set; }

        public string RichTb { get; set; }

        void GetVolume()
        {
            SystemVolumeConfigurator volume = new SystemVolumeConfigurator();

            CurrentValue = volume.GetVolume();

            SystemValue = CurrentValue.ToString();
        }

        //private void OnServerMessage(NamedPipeConnection<string, string> connection, string message)
        //{
        //    //richTextBox.Dispatcher.Invoke(new Action(delegate
        //    //{
        //    //    AddLine("Server: " + message);
        //    //}));

        //    RichTb += "Server: " + message;
        //}

        //private void OnDisconnected(NamedPipeConnection<string, string> connection)
        //{
        //    //richTextBox.Dispatcher.Invoke(new Action(delegate
        //    //{
        //    //    AddLine("Disconnected from server");
        //    //}));
        //    RichTb += "Disconnected from server";
        //}

        //private void AddLine(string html)
        //{
        //    //richTextBox.Dispatcher.Invoke(new Action(delegate
        //    //{
        //    //    richTextBox.AppendText(Environment.NewLine + html);
        //    //}));

        //    RichTb += Environment.NewLine + html;
        //}

    }
}