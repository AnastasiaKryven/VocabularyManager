using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolumeManagerService.Interfaces;
using VolumeManagerService.Services;

namespace VolumeManagerService.Commands
{
    public class Commander : ICommander
    {
        private ICommand _command;
        private readonly ICommandFactory _commandFactory;

        public Commander(ICommandFactory commandFactory)
        {
            this._commandFactory = commandFactory;
        }

        public void Execute(string message)
        {
            var dictionary = Parser(message);
            foreach (var item in dictionary)
            {
                _command = _commandFactory.SetCommand(item.Key);
                _command.Execute(item.Value);
            }
        }

        private Dictionary<string, string> Parser(string message)
        {
            return JsonConvert.DeserializeObject<Dictionary<string, string>>(message);
        }
    }
}
