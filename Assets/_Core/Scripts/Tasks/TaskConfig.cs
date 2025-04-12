﻿using System.Collections.Generic;
using _Core.Scripts.Employees;
using UnityEngine;

namespace _Core.Scripts.Tasks
{
    [CreateAssetMenu(fileName = "New Task", menuName = "Create new Task")]
    public class TaskConfig : ScriptableObject
    {
        public string id;
        public string name;
        public string text;
        public TaskDifficultyConfig difficultyConfig;
        public string comment;
        public int turnNumber;

        public List<CharacterAttribute> conditions;
        public List<RewardAttribute> rewards;
        
        // public TaskRewardConfig rewards;
    }
}