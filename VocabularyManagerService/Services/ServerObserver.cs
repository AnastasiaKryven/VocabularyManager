using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyManagerService.Interfaces;

namespace VocabularyManagerService.Services
{
    public class ServerObserver : IPipeStreamObserver<string>
    {
        SystemVolumeService volumeService = new SystemVolumeService();

        public void OnNext(string value)
        {
            
            //volumeService.SetVolume(Convert.ToSingle(value));
            
            Console.WriteLine(value);

            PipeStream.Write(value);

            
        }

        public void OnError(Exception error)
        {

        }

        public void OnCompleted()
        {

        }

        public PipeStream PipeStream { get; set; }

        public void OnConnected()
        {

        }
    }
}
