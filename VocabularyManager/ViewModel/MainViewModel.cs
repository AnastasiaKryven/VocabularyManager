using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using NamedPipeWrapper;
using VocabularyManager.Services;
using VolumeManager.Models;
using VolumeManagerService.Services;

namespace VolumeManager.ViewModel
{

    public class MainViewModel : ViewModelBase
    {
        ConnectionManagement connection = new ConnectionManagement();

        private ObservableCollection<VolumeModel> volumeModels;
        private VolumeModel volumeModel;
        private float currentValue;
        private string mainTextBox;

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

        public string MainTextBox
        {
            get { return this.mainTextBox; }
            set
            {
                mainTextBox = value;
                RaisePropertyChanged("MainTextBox");
            }
        }

        public MainViewModel()
        {
            VolumeModel = new VolumeModel();
            Volume = new ObservableCollection<VolumeModel>();
            ApplyCommand = new RelayCommand(Apply);
            GetVolumeCommand = new RelayCommand(GetVolume);
            connection.Message += Display;
        }

        private void Display(string message)
        {
            MainTextBox += Environment.NewLine + "Server: " + message;
            SystemValue = message;
        }

        private void Apply()
        {
            connection.SendMessage(currentValue.ToString());
        }

        private void GetVolume()
        {
            connection.SendMessage(SystemValue);
        }
    }
}