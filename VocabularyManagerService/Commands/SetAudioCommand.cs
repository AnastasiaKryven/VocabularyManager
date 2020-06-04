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
        private string message;
        private ISystemVolumeService _volumeService = new SystemVolumeService();

        public SetAudioCommand()
        {
        }

        public void Execute(string message)
        {
            this.message = message;
            Console.WriteLine("Command " + message);
            _volumeService.SetVolume(Convert.ToInt32(message));
        }
    }
}
