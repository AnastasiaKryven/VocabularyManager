using System;
using System.Collections.Generic;
using VocabularyManagerService.Interfaces;
using VocabularyManagerService.Services;
using VolumeManagerService.Services;

namespace VocabularyManagerService.Commands
{
    public class CommandFactory : ICommandFactory
    {
        private readonly IDictionary<string, Func<ICommand>> _commands;

        public CommandFactory(ISystemVolumeService volumeService)
        {
            _commands = new Dictionary<string, Func<ICommand>>
                        {
                            {"Audio", () => new SetAudioCommand(volumeService)}
                        };
        }

        public ICommand SetCommand(string type)
        {
            _commands.TryGetValue(type, out Func<ICommand> command);
            return command();
        }

    }
}
