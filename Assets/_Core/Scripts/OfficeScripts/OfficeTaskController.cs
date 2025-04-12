using System.Collections.Generic;
using _Core.Scripts.OfficeScripts.View;
using _Core.Scripts.Tasks;
using _Core.Scripts.Tasks.View;
using _Core.Scripts.TurnManagerScripts;
using _Core.StaticProvider;
using R3;
using UnityEngine;
using VContainer;

namespace _Core.Scripts.OfficeScripts
{
    public class OfficeTaskController
    {
        [Inject] private OfficeView m_officeView;
        [Inject] private TaskView m_taskView;
        [Inject] private TurnManager m_turnManager;

        private List<TaskConfig> m_allTaskConfigs;
        private List<TaskButton> m_taskButtons;

        private TaskConfig _taskConfig;
        private CompositeDisposable _disposable;

        public OfficeTaskController()
        {
            m_allTaskConfigs = new List<TaskConfig>();

            m_allTaskConfigs.AddRange(StaticDataProvider.Get<TaskDataProvider>().asset.tasks);

            _disposable = new CompositeDisposable();

            m_taskButtons = new List<TaskButton>();
        }

        public void Init()
        {
            m_officeView.TaskButtons.ForEach(button => button.Disable());

            m_turnManager.OnTurnStarted
                .Subscribe(_ =>
                {
                    CreateTasks();
                })
                .AddTo(_disposable);

            m_turnManager.OnTurnEnded
                .Subscribe(_ =>
                {
                    ResetTask();
                })
                .AddTo(_disposable);
        }

        public void Disable()
        {
            _disposable?.Dispose();
        }

        public void OpenTask(TaskConfig selectedTask)
        {
            m_taskView.Open();
            m_taskView.Name = selectedTask.name;
            m_taskView.Description = selectedTask.text;
        }

        private void CreateTasks()
        {
            m_taskButtons.AddRange(m_officeView.TaskButtons);

            for (int i = 0; i < 3; i++)
            {
                TaskButton taskButton = m_taskButtons[Random.Range(0, m_taskButtons.Count)];
                m_taskButtons.Remove(taskButton);

                taskButton.Init(GetRandomTask());

                taskButton.TaskSelected += OpenTask;
            }
        }

        private void ResetTask()
        {
            m_taskButtons.ForEach(button =>
            {
                button.Disable();

                button.TaskSelected -= OpenTask;
            });
        }

        private TaskConfig GetRandomTask()
        {
            if (m_allTaskConfigs.Count == 0)
            {
                m_allTaskConfigs.AddRange(StaticDataProvider.Get<TaskDataProvider>().asset.tasks);
            }

            TaskConfig randomConfig = m_allTaskConfigs[Random.Range(0, m_allTaskConfigs.Count)];

            m_allTaskConfigs.Remove(randomConfig);

            return randomConfig;
        }
    }
}