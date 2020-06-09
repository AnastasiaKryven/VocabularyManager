using VolumeManagerService.Interfaces;

namespace VolumeManagerService.Commands
{
    public interface ICommandFactory
    {
        ICommand SetCommand(string type);
    }
}