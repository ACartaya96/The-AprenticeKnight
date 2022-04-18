using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    private static readonly string FirstPlay = "FirstPlay";
    private static readonly string MasterPref = "MasterPref";
    private static readonly string PlayEffectsPref = "PlayEffectsPref";
    private static readonly string GameEffectsPref = "GameEffectsPref";
    private static readonly string BackgroundPref = "BackgroundPref";
    private int firstPlayInt;
    public Slider MasterSlider, PlayEffectsSlider, GameEffectsSlider, BackgroundSlider;
    private float MasterFloat, PlayEffectsFloat, GameEffectsFloat, BackgroundFloat;
    public AudioMixer audioMixer;

    void Start()
    {
        firstPlayInt = PlayerPrefs.GetInt(FirstPlay);
        if(firstPlayInt == 0)
        {
            MasterFloat = 0.125f;
            PlayEffectsFloat = 0.50f;
            GameEffectsFloat = 0.50f;
            BackgroundFloat = 0.25f;
            MasterSlider.value = MasterFloat;
            PlayEffectsSlider.value = PlayEffectsFloat;
            GameEffectsSlider.value = GameEffectsFloat;
            BackgroundSlider.value = BackgroundFloat;
            PlayerPrefs.SetFloat(MasterPref, MasterFloat);
            PlayerPrefs.SetFloat(PlayEffectsPref, PlayEffectsFloat);
            PlayerPrefs.SetFloat(GameEffectsPref, GameEffectsFloat);
            PlayerPrefs.SetFloat(BackgroundPref, BackgroundFloat);
            PlayerPrefs.SetInt(FirstPlay, -1);
        }
        else
        {
            MasterFloat = PlayerPrefs.GetFloat(MasterPref);
            MasterSlider.value = MasterFloat;
            PlayEffectsFloat = PlayerPrefs.GetFloat(PlayEffectsPref);
            PlayEffectsSlider.value = PlayEffectsFloat;
            GameEffectsFloat = PlayerPrefs.GetFloat(GameEffectsPref);
            GameEffectsSlider.value = GameEffectsFloat;
            BackgroundFloat = PlayerPrefs.GetFloat(BackgroundPref);
            BackgroundSlider.value = BackgroundFloat;
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
        PlayerPrefs.SetFloat(MasterPref, MasterSlider.value);
        PlayerPrefs.SetFloat(PlayEffectsPref, PlayEffectsSlider.value);
        PlayerPrefs.SetFloat(GameEffectsPref, GameEffectsSlider.value);
        PlayerPrefs.SetFloat(BackgroundPref, BackgroundSlider.value);
    }

    void OnApplicationFocus(bool inFocus)
    {
        if(!inFocus)
        {
            SaveSoundSettings();
        }
    }

    public void UpdateSound()
    {
        
    }





}
