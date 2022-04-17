using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixer audioMixer;


    public void SetMasterAudio (float volume)
    {
        audioMixer.SetFloat("MasterVolume", volume);
    }

    public void SetPlayerEffectsAudio (float volume)
    {
        audioMixer.SetFloat("PlayerEffectsVolume", volume);
    }

    public void SetGameEffectsAudio (float volume)
    {
        audioMixer.SetFloat("GameEffectsVolume", volume);
    }

    public void SetBackgroundAudio (float volume)
    {
        audioMixer.SetFloat("BackgroundVolume", volume);
    }






}
