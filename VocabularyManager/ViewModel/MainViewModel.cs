using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using NamedPipeWrapper;
using VolumeManager.Models;
using VolumeManager.Services;

namespace VolumeManager.ViewModel
{

    public class MainViewModel : ViewModelBase
    {
        private readonly IConnectionManagement _connection;

        private int _currentValue;
        private Volume volume;

        public int CurrentValue { get; set; }
        public string SystemValue { get; set; }

        public string MainTextBox { get; set; }

        public RelayCommand ApplyCommand { get; set; }

        public RelayCommand GetVolumeCommand { get; set; }

        public MainViewModel(IConnectionManagement connectionManagement)
        {
            this._connection = connectionManagement;
            ApplyCommand = new RelayCommand(Apply);
            _connection.Message += Display;
        }

        private void Display(string message)
        {
            MainTextBox += Environment.NewLine + "Server: " + message;
            CurrentValue = Convert.ToInt32(message);
            SystemValue = message;

            RaisePropertyChanged(() => CurrentValue);
            RaisePropertyChanged(() => SystemValue);
            RaisePropertyChanged(() => MainTextBox);
        }

        private void Apply()
        {
            volume = new Volume
            {
                Audio = _currentValue.ToString()
            };
            _connection.SendMessage(volume);
        }
    }
}