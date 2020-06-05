using VocabularyManagerService.Interfaces;

namespace VocabularyManagerService.Commands
{
    public interface ICommandFactory
    {
        ICommand SetCommand(string type);
    }
}