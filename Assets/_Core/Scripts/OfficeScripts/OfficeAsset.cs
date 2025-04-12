using System.Collections.Generic;
using UnityEngine;

namespace _Core.Scripts.OfficeScripts
{
    [CreateAssetMenu(fileName = "TurnAsset", menuName = "Create new TurnAsset")]
    public class OfficeAsset : ScriptableObject
    {
        public List<OfficeConfig> configs;
    }
}