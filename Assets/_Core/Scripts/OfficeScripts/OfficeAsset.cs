using System.Collections.Generic;
using UnityEngine;

namespace _Core.Scripts.OfficeScripts
{
    [CreateAssetMenu(fileName = "OfficeAsset", menuName = "Create new OfficeAsset")]
    public class OfficeAsset : ScriptableObject
    {
        public List<OfficeConfig> configs;

        [Header("Endings")]
        public List<EndingConfig> endingsByGameStat;

        public string endingLose;
    }
}