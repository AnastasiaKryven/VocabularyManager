using System;
using System.ServiceProcess;
using VolumeManagerService.Services;

namespace VolumeManagerService
{
    public partial class VolumeManagerService : ServiceBase
    {
        private readonly IConnectionManagement _connection;
        private static INotifyManager _manager;

        public VolumeManagerService(IConnectionManagement connection, INotifyManager manager)
        {
            this._connection = connection;
            _manager = manager;
        }

        protected override void OnStart(string[] args)
        {
            _connection.Start();
        }

        protected override void OnStop()
        {
            _connection.Stop();
        }
    }
}

