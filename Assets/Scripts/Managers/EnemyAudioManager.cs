using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TAK
{
    public class EnemyAudioManager : AudioManager
    {
        public SFXEvent footStepSFX;
        public SFXEvent weaponSwingSFX;

        public void PlayFootStepAudio()
        {

            footStepSFX?.Play();
        }

        public void PlaySwingAudio()
        {

        }
    }
}