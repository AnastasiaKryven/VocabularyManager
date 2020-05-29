﻿using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VolumeManagerService.Interfaces
{
    public interface IPipeStreamObserver<in T> : IObserver<T>
    {
        PipeStream PipeStream { get; set; }

        void OnConnected();
    }
}
