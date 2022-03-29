using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Sirenix.OdinInspector;

namespace TAK
{
    [CreateAssetMenu(fileName = "SFX_Event", menuName = "SoundSystem/SFX Event")]
    public class SFXEvent : ScriptableObject
    {
        #region config
        public AudioClip[] clips;
        public Vector2 volume = new Vector2(0.5f, 0.5f);
        public Vector2 pitch = new Vector2(1, 1);

        private int playIndex;
        [SerializeField] SoundClipPlayOrder playOrder;
        #endregion

        #region Preview
        #if UNITY_EDITOR
        private AudioSource previewer;

        private void OnEnable()
        {
            previewer = EditorUtility.CreateGameObjectWithHideFlags("AudioPreview", HideFlags.HideAndDontSave,
                typeof(AudioSource)).GetComponent<AudioSource>();
        }

        private void OnDisable()
        {
            DestroyImmediate(previewer.gameObject);
        }

        [ButtonGroup("Preview Controls")]
        [GUIColor(.3f,1f,.3f)]
        [Button(ButtonSizes.Gigantic)]
        private void PlayPreview()
        {
            Play(previewer);
        }
        [ButtonGroup("Preview Controls")]
        [GUIColor(1f, .3f, .3f)]
        [Button(ButtonSizes.Gigantic)]
        [EnableIf("@previewer.isPlaying")]
        private void StopPreview()
        {
            previewer.Stop();
        }
    #endif
    #endregion

        private AudioClip GetAudioClip()
        {

            var clip = clips[playIndex >= clips.Length ? 0 : playIndex];

            switch(playOrder)
            {
                case SoundClipPlayOrder.InOrder:
                    playIndex = (playIndex + 1) % clips.Length;
                break;
                case SoundClipPlayOrder.Random:
                    playIndex = Random.Range(0, clips.Length);
                break;
                case SoundClipPlayOrder.Reversed:
                    playIndex = (playIndex - 1) % clips.Length;
                break;
            }
            return clip;
        }

        public AudioSource Play(AudioSource audioSourceParam = null)
        {
            if(clips.Length == 0)
            {
                //this.LogWarning($"Missing sound clips for {name}");
                return null;
            }
            var source = audioSourceParam;
            if(source == null)
            {
                var obj = new GameObject("Sound", typeof(AudioSource));
            }

            source.clip = clips[0];
            source.volume = Random.Range(volume.x, volume.y);
            source.pitch = Random.Range(pitch.x, pitch.y);

            source.Play();
#if UNITY_EDITOR
            if (source != previewer)
            {
                Destroy(source.gameObject, source.clip.length / source.pitch);
            }
#else
            Destroy(source.gameObject, source.clip.length / source.pitch);
#endif

            return source;
        }

        enum SoundClipPlayOrder
        {
            Random,
            InOrder,
            Reversed
        }
    }
}
