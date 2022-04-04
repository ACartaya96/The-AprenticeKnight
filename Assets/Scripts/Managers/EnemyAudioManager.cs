using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace TAK
{
    public class EnemyAudioManager : AudioManager
    {
        public SFXEvent footStepSFX;
        public SFXEvent weaponSwingSFX;
        public SFXEvent injuredScreamSFX;
        public SFXEvent swipeAttackSFX;
        public SFXEvent bigSlamAttackSFX;
        public SFXEvent spinAttackSFX;

        public void PlayFootStepAudio()
        {
            footStepSFX?.Play();
        }

        public void PlaySwipeAttackAudio()
        {
            swipeAttackSFX.Play();
        }

        public void PlayInjuredAudio()
        {
            injuredScreamSFX.Play();
        }

        public void PlayBigSlamAudio()
        {
            bigSlamAttackSFX.Play();
        }

        public void PlaySpinAttackAudio()
        {
            spinAttackSFX.Play();
        }
    }
}