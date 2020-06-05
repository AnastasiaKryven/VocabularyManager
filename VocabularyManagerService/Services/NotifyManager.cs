using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VolumeManagerService.Services;

namespace VolumeManagerService.Services
{
    public class NotifyManager : INotifyManager
    {
        private IConnectionManagement _connection;

        public NotifyManager(IConnectionManagement connection)
        {
            this._connection = connection;
        }

        public void Send(string message)
        {
            _connection.SendMessage(message);
        }
    }
}
