using System.Collections.Generic;
using _Core.Scripts.Tasks;
using UnityEngine;

namespace _Core.Scripts.TurnManagerScripts
{
    [CreateAssetMenu(fileName = "TurnAsset", menuName = "Create new TurnAsset")]
    public class TurnAsset : ScriptableObject
    {
        public List<TurnConfig> turns;
    }
}