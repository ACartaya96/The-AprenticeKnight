using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Sirenix.OdinInspector;

namespace TAK
{
    public enum BlendingType
    {
        Additive,
        Single,
    }
    [CreateAssetMenu(fileName = "Music", menuName = "SoundSystem/Music Event")]
    public class MusicEvent : ScriptableObject
    {
        
        [InlineEditor]
        [SerializeField] AudioClip[] musicLayers;
       
        [SerializeField] BlendingType blendingType = BlendingType.Additive;
       
        [SerializeField] AudioMixerGroup mixer;

        public AudioClip[] MusicLayer => musicLayers;
        public BlendingType BlendingType => blendingType;
        public AudioMixerGroup Mixer => mixer;
    }
}
