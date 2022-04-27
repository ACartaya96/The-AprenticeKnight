using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace TAK
{
public class AudioMixerMaster : MonoBehaviour
{
    private static readonly string FirstPlay = "FirstPlay";
    private static readonly string MasterPref = "MasterPref";
    private static readonly string PlayEffectsPref = "PlayEffectsPref";
    private static readonly string GameEffectsPref = "GameEffectsPref";
    private static readonly string BackgroundPref = "BackgroundPref";
    //private int firstPlayInt;
    public Slider MasterSlider, PlayEffectsSlider, GameEffectsSlider, BackgroundSlider;
    //public float MasterFloat, PlayEffectsFloat, GameEffectsFloat, BackgroundFloat;

    public FloatContainer MasterFloat, PlayEffectsFloat, GameEffectsFloat, BackgroundFloat;

    public IntContainer firstPlayInt;
    public AudioMixer audioMixer;

    void Start()
    {
         //Debug.Log(firstPlayInt.currentInt);
         firstPlayInt.currentInt = PlayerPrefs.GetInt(FirstPlay);
         if(firstPlayInt.currentInt == 0)
         {
            MasterFloat.currentFloat = MasterFloat.MaxFloat;
            PlayEffectsFloat.currentFloat = PlayEffectsFloat.MaxFloat;
            GameEffectsFloat.currentFloat = GameEffectsFloat.MaxFloat;
            BackgroundFloat.currentFloat = BackgroundFloat.MaxFloat;
            MasterSlider.value = MasterFloat.currentFloat;
            PlayEffectsSlider.value = PlayEffectsFloat.currentFloat;
            GameEffectsSlider.value = GameEffectsFloat.currentFloat;
            BackgroundSlider.value = BackgroundFloat.currentFloat;
            PlayerPrefs.SetFloat(MasterPref, MasterFloat.currentFloat);
            PlayerPrefs.SetFloat(PlayEffectsPref, PlayEffectsFloat.currentFloat);
            PlayerPrefs.SetFloat(GameEffectsPref, GameEffectsFloat.currentFloat);
            PlayerPrefs.SetFloat(BackgroundPref, BackgroundFloat.currentFloat);
            PlayerPrefs.SetInt(FirstPlay, (firstPlayInt.currentInt -1));
         }
         else
        {
            // PlayerPrefs.SetFloat(MasterPref, MasterFloat);
            // PlayerPrefs.SetFloat(PlayEffectsPref, PlayEffectsFloat);
            // PlayerPrefs.SetFloat(GameEffectsPref, GameEffectsFloat);
            // PlayerPrefs.SetFloat(BackgroundPref, BackgroundFloat);
            MasterFloat.currentFloat = PlayerPrefs.GetFloat(MasterPref);
            MasterSlider.value = MasterFloat.currentFloat;
            PlayEffectsFloat.currentFloat = PlayerPrefs.GetFloat(PlayEffectsPref);
            PlayEffectsSlider.value = PlayEffectsFloat.currentFloat;
            GameEffectsFloat.currentFloat = PlayerPrefs.GetFloat(GameEffectsPref);
            GameEffectsSlider.value = GameEffectsFloat.currentFloat;
            BackgroundFloat.currentFloat = PlayerPrefs.GetFloat(BackgroundPref);
            BackgroundSlider.value = BackgroundFloat.currentFloat;
            // MasterSlider.value = MasterFloat;
            // PlayEffectsSlider.value = PlayEffectsFloat;
            // GameEffectsSlider.value = GameEffectsFloat;
            // BackgroundSlider.value = BackgroundFloat;
            
        }
    }

    public void SetMasterAudio (float volume)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(volume) * 20);
    }

    public void SetPlayerEffectsAudio (float volume)
    {
        audioMixer.SetFloat("PlayerEffectsVolume", Mathf.Log10(volume) * 20);
    }

    public void SetGameEffectsAudio (float volume)
    {
        audioMixer.SetFloat("GameEffectsVolume", Mathf.Log10(volume) * 20);
    }

    public void SetBackgroundAudio (float volume)
    {
        audioMixer.SetFloat("BackgroundVolume", Mathf.Log10(volume) * 20);
    }

    public void SaveSoundSettings()
    {
        Debug.Log("SettingSaved");
        PlayerPrefs.SetFloat(MasterPref, MasterSlider.value);
        PlayerPrefs.SetFloat(PlayEffectsPref, PlayEffectsSlider.value);
        PlayerPrefs.SetFloat(GameEffectsPref, GameEffectsSlider.value);
        PlayerPrefs.SetFloat(BackgroundPref, BackgroundSlider.value);
    }

    // void OnApplicationFocus(bool inFocus)
    // {
    //     if(!inFocus)
    //     {
    //         SaveSoundSettings();
    //     }
    // }





}
}
