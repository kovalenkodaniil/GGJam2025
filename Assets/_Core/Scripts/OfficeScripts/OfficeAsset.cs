using System.Collections.Generic;
using UnityEngine;

namespace _Core.Scripts.OfficeScripts
{
    [CreateAssetMenu(fileName = "OfficeAsset", menuName = "Create new OfficeAsset")]
    public class OfficeAsset : ScriptableObject
    {
        public List<OfficeConfig> configs;
    }
}