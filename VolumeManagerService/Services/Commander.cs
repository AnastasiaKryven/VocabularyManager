using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VocabularyManagerService.Commands;
using VocabularyManagerService.Interfaces;
using VocabularyManagerService.Models;

namespace VocabularyManagerService.Services
{
    public class Commander
    {
        private ICommand _command;

        public void SetCommand(ICommand command)
        {
            this._command = command;
        }

        public void Start()
        {
            if (_command != null)
                _command.Execute();
        }

        public void Stop()
        {
            if (_command != null)
                _command.Undo();
        }
        //private void _connection_VolumeData(string data)
        //{
        //    var regex = new Regex("^[0-9]+$");
        //    //var recognizeData 
        //    if (regex.IsMatch(data))
        //    {
        //        new ChangeVolumeCommand(data);
        //    }
        //}
    }
}
