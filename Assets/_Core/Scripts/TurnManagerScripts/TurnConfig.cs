using System;
using System.Collections.Generic;
using _Core.Scripts.Tasks;

namespace _Core.Scripts.TurnManagerScripts
{
    [Serializable]
    public class TurnConfig
    {
        public string id;
        public int turnNumber;

        public List<TaskData> possibleTasks;
        public List<TaskDifficulty> taskDifficulties;
    }
}