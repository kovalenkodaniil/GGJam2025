using System.Collections.Generic;
using _Core.Scripts.OfficeScripts.View;
using _Core.Scripts.Tasks;
using _Core.Scripts.Tasks.View;
using _Core.StaticProvider;
using UnityEngine;
using VContainer;

namespace _Core.Scripts.OfficeScripts
{
    public class OfficeTaskController
    {
        [Inject] private OfficeView m_officeView;
        [Inject] private TaskView m_taskView;

        private List<TaskConfig> _allTaskConfigs;

        private TaskConfig _taskConfig;

        public OfficeTaskController()
        {
            _allTaskConfigs = new List<TaskConfig>();

            _allTaskConfigs.AddRange(StaticDataProvider.Get<TaskDataProvider>().asset.tasks);
        }

        public void Init()
        {
            m_officeView.TackClicked += OpenTask;
        }

        public void Disable()
        {
            m_officeView.TackClicked -= OpenTask;
        }

        public void OpenTask()
        {
            GetRandomTask();

            m_taskView.Open();
            m_taskView.Name = _taskConfig.name;
            m_taskView.Description = _taskConfig.text;
        }

        private void GetRandomTask()
        {
            _taskConfig = _allTaskConfigs[Random.Range(0, _allTaskConfigs.Count)];
        }
    }
}