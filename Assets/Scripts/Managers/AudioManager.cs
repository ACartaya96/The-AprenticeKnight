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
            audioSource.pitch = Random.Range(0.5f, 1);
            audioSource.volume = Random.Range(0.75f, 1);
            audioSource.PlayOneShot(targetSFX);
            
        }
    }
}
