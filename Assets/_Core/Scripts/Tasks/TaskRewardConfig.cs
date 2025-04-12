using System;
using System.Collections.Generic;
using UnityEngine;

namespace _Core.Scripts.Tasks
{
    [CreateAssetMenu(fileName = "New Reward", menuName = "Create new Reward")]
    public class TaskRewardConfig : ScriptableObject
    {
        public string id;
        public List<RewardAttribute> rewardAttributes;
    }
    
    public enum EnumReward
    {
        Graphic,
        Popularity,
        Quality,
        Gameplay,
        Profit
    }
    
    [Serializable]
    public class RewardAttribute
    {
        public EnumReward type;
        public int value;

        public RewardAttribute() {}
        public RewardAttribute(EnumReward type, int value)
        {
            this.type = type;
            this.value = value;
        }
    }

}