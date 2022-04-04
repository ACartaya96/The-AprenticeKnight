using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TAK
{
    public class PlayerAudioManager : AudioManager
    {
        public SFXEvent footStepSFX;
        public SFXEvent SwingSFX;

        public void PlayFootStepAudio()
        {
           
            footStepSFX?.Play();
        }

        public void PlaySwingAudio()
        {
            SwingSFX.Play();
        }


    }
}

