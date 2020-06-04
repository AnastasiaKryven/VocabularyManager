using System;
using System.Collections.Generic;
using VocabularyManagerService.Interfaces;

namespace VocabularyManagerService.Commands
{
    public class CommandFactory
    {
        private readonly IDictionary<string, Func<ICommand>> _commands;

        public CommandFactory()
        {
            _commands = new Dictionary<string, Func<ICommand>>
                        {
                            {"Audio", () => new SetAudioCommand()},
                            //{"AudioServerValue", () => new GetAudioCommand()}
                        };
        }

        public ICommand SetCommand(string type)
        {
            _commands.TryGetValue(type, out Func<ICommand> command);
            return command();
        }

    }
}
