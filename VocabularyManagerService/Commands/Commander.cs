using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyManagerService.Interfaces;
using VocabularyManagerService.Services;

namespace VocabularyManagerService.Commands
{
    public class Commander : ICommander
    {
        private ICommand _command;

        public Commander()
        {
        }

        public void Execute(string message)
        {
            var dictionary = Parser(message);
            foreach (var item in dictionary)
            {
                _command = new CommandFactory().SetCommand(item.Key);
                _command.Execute(item.Value);
            }
        }

        private Dictionary<string, string> Parser(string message)
        {
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(message);
        }
    }
}
