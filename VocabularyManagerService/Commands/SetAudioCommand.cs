using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolumeManagerService.Interfaces;
using VolumeManagerService.Services;

namespace VolumeManagerService.Commands
{
    public class SetAudioCommand : ICommand
    {
        private readonly ISystemVolumeService _volumeService;

        public SetAudioCommand(ISystemVolumeService volumeService)
        {
            _volumeService = volumeService;
        }

        public void Execute(string message)
        {
            Console.WriteLine("Command " + message);
            _volumeService.SetVolume(Convert.ToInt32(message));
        }
    }
}
