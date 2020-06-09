using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolumeManagerService.Interfaces
{
    public interface ICommand
    {
        void Execute(string message);
    }
}
