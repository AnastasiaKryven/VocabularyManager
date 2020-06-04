using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyManagerService.Interfaces;
using VocabularyManagerService.Models;
using VocabularyManagerService.Services;

namespace VocabularyManagerService.Commands
{
    public class GetAudioCommand : ICommand
    {
        private IConnectionManagement _connection;
        Volume volume = new Volume();

        public GetAudioCommand()
        {
        }

        public void Execute(string message)
        {
            volume.AudioServerValue = message;
            var json = JsonConvert.SerializeObject(volume);
            _connection.SendMessage(json);
        }
    }
}
