using System;
using System.ServiceProcess;
using VolumeManagerService.Services;

namespace VolumeManagerService
{
    public partial class VolumeManagerService : ServiceBase
    {
        private readonly IConnectionManagement _connection;

        public VolumeManagerService(IConnectionManagement connection)
        {
            this._connection = connection;
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

