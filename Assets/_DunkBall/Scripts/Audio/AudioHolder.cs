using System;
using System.Linq;
using UnityEngine;

namespace _DunkBall.Scripts.Audio
{
    [CreateAssetMenu(fileName = "AudioHolder", menuName = "Holders/AudioHolder")]
    public class AudioHolder : ScriptableObject
    {
        [SerializeField] private float[] _musicVolumes;
        [SerializeField] private float[] _soundVolumes;
        [SerializeField] private AudioClip[] _clips;

        public AudioClip GetAudioClipByName(string clipName) => _clips.First(x =>
            string.Equals(x.name, clipName, StringComparison.CurrentCultureIgnoreCase));

        public float GetMusicVolume(int index) => _musicVolumes[index];
        public float GetSoundVolume(int index) => _soundVolumes[index];
    }
}