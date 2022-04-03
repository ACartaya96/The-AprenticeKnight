using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TAK
{
    public class AudioManager : MonoBehaviour
    {
        public List<AudioClip> musciClips = new List<AudioClip>();
        //public List<SFXEvent> sfxClips = new List<SFXEvent>();

        public AudioSource audioSource;
        private void Awake()
        {
            audioSource = GetComponent<AudioSource>();
        }
        public void PlayTargetSoundEffect(AudioClip targetSFX)
        {

        }
    }
}
