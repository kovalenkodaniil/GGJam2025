using System;
using System.Collections.Generic;
using _Core.Scripts.Employees;
using UnityEngine;
using Random = UnityEngine.Random;

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
        public List<AudioClip> legendaryClips;

        public AudioClip GetClipByGender(EnumGender gender)
        {
            return gender switch
            {
                EnumGender.Man => GetRandomManClip(),
                EnumGender.Female => GetRandomWomenClip(),
                EnumGender.Ilya => GetRandomLegendaryClip(),
                _ => throw new ArgumentOutOfRangeException(nameof(gender), gender, null)
            };
        }

        public AudioClip GetRandomManClip()
        {
            return mansClips[Random.Range(0, mansClips.Count)];
        }

        public AudioClip GetRandomWomenClip()
        {
            return womenClips[Random.Range(0, womenClips.Count)];
        }

        public AudioClip GetRandomLegendaryClip()
        {
            return legendaryClips[Random.Range(0, womenClips.Count)];
        }
    }
}