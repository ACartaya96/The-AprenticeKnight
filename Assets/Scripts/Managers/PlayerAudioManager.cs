using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TAK
{
    public class PlayerAudioManager : AudioManager
    {
        public SFXEvent footStepSFX;
        public SFXEvent SwingSFX;
        public SFXEvent RollSFX;
        public SFXEvent JumpSFX;
        public SFXEvent deathSFX;
        public SFXEvent outOfManaSFX;
        //public SFXEvent HurtSFX;Hurt sound effect that Alex already hardcoded. Commented in case we want to make it animator based.


        public void PlayFootStepAudio()
        {
           
            footStepSFX?.Play();
        }

        public void PlaySwingAudio()
        {
            SwingSFX.Play();
        }

        public void PlayRollAudio()
        {
            RollSFX.Play();
        }
        
        public void PlayJumpAudio()
        { 
            JumpSFX.Play();
        }

        public void PlayDeathAudio()
        {
            deathSFX.Play();
        }

        public void PlayOutOfManaAudio()
        {
            outOfManaSFX.Play();
        }
        // public void PlayHurtAudio()
        //{
        //  HurtSFX.Play();
        //}



    }

}

