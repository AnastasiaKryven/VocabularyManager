using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyManagerService.Interfaces;
using VocabularyManagerService.Services;

namespace VocabularyManagerService.Commands
{
    public class GetAudioCommand //: ICommand
    {
        private IConnectionManagement _connection;

        public GetAudioCommand()
        {
        }

        public void Execute(string message)
        {
            _connection.SendMessage(message);
        }
    }
}
