using UnityEngine;

namespace _Core.Scripts.Tasks
{
    [CreateAssetMenu(fileName = "New TaskDifficulty", menuName = "Create new TaskDifficulty")]
    public class TaskDifficultyConfig : ScriptableObject
    {
        public string id;
        public TaskDifficulty difficulty;
        public int experience;
    }
    
    public enum TaskDifficulty
    {
        Easy,
        Medium,
        Hard,
    }
}