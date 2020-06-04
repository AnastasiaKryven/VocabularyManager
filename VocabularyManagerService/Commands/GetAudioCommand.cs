using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyManagerService.Interfaces;
using VocabularyManagerService.Services;

namespace VocabularyManagerService.Commands
{
    public class GetAudioCommand : ICommand
    {
        private string _message;
        private IConnectionManagement _connection;

        public GetAudioCommand(string message)
        {
            this._message = message;
        }

        public void Execute()
        {
            _connection.SendMessage(_message);
        }
    }
}
