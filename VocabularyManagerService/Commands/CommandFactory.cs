using System;
using System.Collections.Generic;
using VocabularyManagerService.Interfaces;
using VocabularyManagerService.Services;

namespace VocabularyManagerService.Commands
{
    public class CommandFactory : ICommandFactory
    {
        private readonly IDictionary<string, Func<ICommand>> _commands;

        public CommandFactory(ICommand command)
        {
            _commands = new Dictionary<string, Func<ICommand>>
                        {
                            {"Audio", () => new SetAudioCommand()}
                        };
        }

        public ICommand SetCommand(string type)
        {
            _commands.TryGetValue(type, out Func<ICommand> command);
            return command();
        }

    }
}
