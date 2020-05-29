using NAudio.CoreAudioApi;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VocabularyManagerService.Interfaces
{
    public interface ISystemVolumeService
    {
        float GetVolume();

        void SetVolume(float volumeLevel);
    }
}
