namespace VocabularyManagerService.Commands
{
    public interface ICommander
    {
        void Execute(string message);
    }
}