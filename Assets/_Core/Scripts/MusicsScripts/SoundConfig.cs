using System.Collections.Generic;
using UnityEngine;

namespace _Core.Scripts
{
    [CreateAssetMenu(fileName = "SoundConfig", menuName = "Create new SoundConfig")]
    public class SoundConfig : ScriptableObject
    {
        public AudioClip mainTheme;
        public AudioClip buttonClick;
        public AudioClip stampClick;
        public AudioClip taskCreated;
        public AudioClip cardInTask;
        public AudioClip cardStartDraging;

        public List<AudioClip> mansClips;
        public List<AudioClip> womenClips;

        public AudioClip GetRandomManClip()
        {
            return mansClips[Random.Range(0, mansClips.Count)];
        }

        public AudioClip GetRandomWomenClip()
        {
            return womenClips[Random.Range(0, womenClips.Count)];
        }
    }
}