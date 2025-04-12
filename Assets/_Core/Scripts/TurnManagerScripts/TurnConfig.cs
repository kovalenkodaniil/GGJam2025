using System.Collections.Generic;
using _Core.Scripts.Tasks;

namespace _Core.Scripts.TurnManagerScripts
{
    public class TurnConfig
    {
        public string id;
        public int turnNumber;
        
        public List<TaskConfig> possibleTasks;
        public List<TaskDifficulty> taskDifficulties;
    }
}