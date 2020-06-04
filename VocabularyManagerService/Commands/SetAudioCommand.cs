using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyManagerService.Interfaces;
using VolumeManagerService.Services;

namespace VocabularyManagerService.Commands
{
    public class SetAudioCommand : ICommand
    {
        private string _message;
        private ISystemVolumeService _volumeService;

        public SetAudioCommand(string message)
        {
            _volumeService = new SystemVolumeService();
            this._message = message;
        }

        public void Execute()
        {
            Console.WriteLine("Command " + _message);
            _volumeService.SetVolume(Convert.ToInt32(_message));
        }
    }
}
