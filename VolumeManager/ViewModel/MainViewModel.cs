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
        private string _systemValue;
        private string _mainTextBox;

        private Volume _volume;

        public int CurrentValue
        {
            get
            {
                return this._currentValue;
            }
            set
            {
                _currentValue = value;
                RaisePropertyChanged(nameof(CurrentValue));
            }
        }

        public string SystemValue
        {
            get
            {
                return this._systemValue;
            }
            set
            {
                _systemValue = value;
                RaisePropertyChanged(nameof(SystemValue));
            }
        }

        public string MainTextBox
        {
            get { return this._mainTextBox; }
            set
            {
                _mainTextBox = value;
                RaisePropertyChanged(nameof(MainTextBox));
            }
        }

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
            SystemValue = CurrentValue.ToString();
        }

        private void Apply()
        {
            _volume = new Volume
            {
                Audio = _currentValue.ToString()
            };
            _connection.SendMessage(_volume);
        }
    }
}