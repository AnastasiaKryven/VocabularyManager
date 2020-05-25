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

        public void OnNext(string value)
        {
            value += " World!";
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
