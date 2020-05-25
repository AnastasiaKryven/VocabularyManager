using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyManagerService.Interfaces;
using VocabularyManagerService.Services;

namespace VocabularyManager.Services
{
    public class ClientObserver : IPipeStreamObserver<string>
    {

        public void OnNext(string value)
        {
            Console.WriteLine(value);

            PipeStream.Write("Hello ");
        }

        public void OnError(Exception error)
        {
            Console.WriteLine(error);
        }

        public void OnCompleted()
        {

        }

        public PipeStream PipeStream { get; set; }

        public void OnConnected()
        {
            PipeStream.Write("Hello ");
        }
    }
}
