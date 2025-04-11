using _Core.StaticProvider;

namespace _Core.Scripts.Tasks
{
    public class TaskDataProvider : IStaticDataProvider
    {
        public TaskAsset asset;

        public TaskDataProvider(TaskAsset asset)
        {
            this.asset = asset;
        }
    }
}