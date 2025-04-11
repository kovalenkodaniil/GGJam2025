using System.Collections.Generic;
using UnityEngine;

namespace _Core.Scripts.Tasks
{
    [CreateAssetMenu(fileName = "TaskAsset", menuName = "Create new TaskAsset")]
    public class TaskAsset : ScriptableObject
    {
        public List<TaskConfig> tasks;
    }
}