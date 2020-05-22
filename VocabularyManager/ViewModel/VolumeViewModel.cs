using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace VocabularyManager.ViewModel
{
    public class VolumeViewModel: INotifyPropertyChanged
    {
        private float soundValue;

        public event PropertyChangedEventHandler PropertyChanged;

        public float SoundValue {
            get { return soundValue; }
            set
            {
                soundValue = value;
                OnPropertyChanged("SoundValue");
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
