using R3;

namespace _Core.Scripts.Tasks
{
    public class TaskData
    {
        public TaskConfig Config;
        public ReactiveProperty<bool> IsCompleted;

        public TaskData(TaskConfig config)
        {
            Config = config;
            IsCompleted = new ReactiveProperty<bool>(false);
        }
    }
}