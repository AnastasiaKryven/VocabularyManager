using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NamedPipeWrapper;

namespace VocabularyManagerService.Services
{
    [Serializable]
    class MyMessage
    {
        public int Id;
        public string Text;

        public override string ToString()
        {
            return string.Format("\"{0}\" (message ID = {1})", Text, Id);
        }
    }
    public class MyServer
    {

        private bool KeepRunning
        {
            get
            {
                var key = Console.ReadKey();
                if (key.Key == ConsoleKey.Q)
                    return false;
                return true;
            }
        }
        public MyServer(string text)
        {
            var server = new NamedPipeServer<MyMessage>(text);
            server.ClientConnected += OnClientConnected;
            server.ClientDisconnected += OnClientDisconnected;
            server.ClientMessage += OnClientMessage;
            server.Error += OnError;
            server.Start();
            while (KeepRunning)
            {
                // Do nothing - wait for user to press 'q' key
            }
            server.Stop();
        }

        private void OnClientConnected(NamedPipeConnection<MyMessage, MyMessage> connection)
        {
            Console.WriteLine("Client {0} is now connected!", connection.Id);
            connection.PushMessage(new MyMessage
            {
                Id = new Random().Next(),
                Text = "Welcome!"
            });
        }

        private void OnClientDisconnected(NamedPipeConnection<MyMessage, MyMessage> connection)
        {
            Console.WriteLine("Client {0} disconnected", connection.Id);
        }

        private void OnClientMessage(NamedPipeConnection<MyMessage, MyMessage> connection, MyMessage message)
        {
            Console.WriteLine("Client {0} says: {1}", connection.Id, message);
        }

        private void OnError(Exception exception)
        {
            Console.Error.WriteLine("ERROR: {0}", exception);
        }
    }
}
