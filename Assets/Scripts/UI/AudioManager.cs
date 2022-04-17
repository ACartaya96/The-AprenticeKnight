using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer actualmasterAudio;

    //public AudioMixer soundeffectAudio;

    //public AudioMixer backgroundAudio;


    public void SetMasterAudio (float volume)
    {
        actualmasterAudio.SetFloat("ActualMasterAudio", volume);
    }




}
