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
        private ISystemVolumeService volumeService;

        public SetAudioCommand(string message)
        {
            volumeService = new SystemVolumeService();
            this.message = message;
        }

        public void Execute()
        {
            Console.WriteLine("Command " + message);
            volumeService.SetVolume(Convert.ToInt32(message));
        }
    }
}
