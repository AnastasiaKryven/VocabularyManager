using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using NamedPipeWrapper;
using VocabularyManager.Models;
using VocabularyManagerService.Services;

namespace VocabularyManager.ViewModel
{

    public class MainViewModel : ViewModelBase
    {
        private ObservableCollection<VolumeModel> volumeModels;
        private readonly NamedPipeClient<string> _client = new NamedPipeClient<string>("nana");
        private VolumeModel volumeModel;
        private float currentValue;
        private string mainTb;

        public ObservableCollection<VolumeModel> Volume
        {
            get { return volumeModels; }
            set
            {
                volumeModels = value;
                RaisePropertyChanged("Volumes");
            }
        }

        public VolumeModel VolumeModel
        {
            get { return volumeModel; }
            set
            {
                volumeModel = value;
                RaisePropertyChanged("Volume");
            }
        }

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

        public string SystemValue { get; set; }

        public string MainTb
        {
            get { return this.mainTb; }
            set
            {
                mainTb = value;
                RaisePropertyChanged("MainTb");
            }
        }

        public MainViewModel()
        {
            _client.ServerMessage += OnServerMessage;
            _client.Disconnected += OnDisconnected;
            _client.Start();

            VolumeModel = new VolumeModel();
            Volume = new ObservableCollection<VolumeModel>();
            ApplyCommand = new RelayCommand(Apply);
            GetVolumeCommand = new RelayCommand(GetVolume);
        }

        private void Apply()
        {
            _client.PushMessage(currentValue.ToString());
        }

        private void GetVolume()
        {
            SystemVolumeService volume = new SystemVolumeService();

            CurrentValue = volume.GetVolume();

            SystemValue = CurrentValue.ToString();

            _client.PushMessage(SystemValue);
        }

        private void OnServerMessage(NamedPipeConnection<string, string> connection, string message)
        {
            MainTb += Environment.NewLine + "Server" + message;
        }

        private void OnDisconnected(NamedPipeConnection<string, string> connection)
        {
            MainTb += Environment.NewLine + "Disconnected from server";
        }
    }
}