﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VocabularyManagerService.Interfaces;
using VocabularyManagerService.Services;

namespace VocabularyManagerService.Commands
{
    public class Commander : ICommand
    {
        private string _message;
        private ICommand _command;
        private IConnectionManagement _connection;

        public Commander(string message)
        {
            this._message = message;
            var dictionary = Parser(message);
            foreach (var item in dictionary)
            _command = SetCommand(item.Key, item.Value);
        }

        public void Execute()
        {
            _command.Execute();
        }

        private Dictionary<string, string> Parser(string message)
        {
           return JsonConvert.DeserializeObject<Dictionary<string, string>>(message);          
        }

        private ICommand SetCommand(string type, string message)
        {
            if (type == "Audio")
            {
               return new SetAudioCommand(message);
            }
            if (type == "AudioServerValue")
            {
                return new GetAudioCommand(message);                   
            }
           
            return new SetAudioCommand(message);
        }
    }
}
