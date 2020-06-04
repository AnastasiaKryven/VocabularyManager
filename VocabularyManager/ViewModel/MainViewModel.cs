using System;
using System.Collections.ObjectModel;
using System.Windows.Controls;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using NamedPipeWrapper;
using VocabularyManager.Models;
using VocabularyManager.Services;
using VolumeManagerService.Services;

namespace VolumeManager.ViewModel
{

    public class MainViewModel : ViewModelBase
    {
        private readonly IConnectionManagement _connection;

        private int _currentValue;
        private string _mainTextBox;
        private Volume volume;

        public int CurrentValue
        {
            get { return _currentValue; }
            set
            {
                _currentValue = value;
                RaisePropertyChanged(nameof(CurrentValue));
            }
        }

        public RelayCommand ApplyCommand { get; set; }

        public RelayCommand GetVolumeCommand { get; set; }

        private string _systemValue;
        public string SystemValue
        {
            get { return _systemValue; }
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
            volume = new Volume
            {
                Audio = _currentValue.ToString()
            };
            _connection.SendMessage(volume);
        }
    }
}