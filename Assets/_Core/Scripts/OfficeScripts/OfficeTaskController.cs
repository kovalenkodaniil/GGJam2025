using System.Collections.Generic;
using _Core.Scripts.Employees;
using System.Linq;
using _Core.Scripts.OfficeScripts.View;
using _Core.Scripts.Tasks;
using _Core.Scripts.Tasks.View;
using _Core.Scripts.TurnManagerScripts;
using _Core.StaticProvider;
using JetBrains.Annotations;
using R3;
using UnityEditor;
using UnityEngine;
using VContainer;

namespace _Core.Scripts.OfficeScripts
{
    public class OfficeTaskController
    {
        [Inject] private OfficeView m_officeView;
        [Inject] private OfficeModel m_officeModel;
        [Inject] private TaskView m_taskView;
        [Inject] private TurnManager m_turnManager;
        [Inject] private TaskModel m_taskModel;

        private List<TaskConfig> m_allTaskConfigs;
        private List<TaskButton> m_taskButtons;

        private TaskConfig _taskConfig;
        private CompositeDisposable _disposable;
        private TaskButton m_currentButton;

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

            SubscribeTaskPanel();

            m_taskModel.IsReady
                .Subscribe(UpdateCompleteButton)
                .AddTo(_disposable);

            m_taskView.OnComplete += CompleteTask;
        }

        public void Disable()
        {
            m_taskView.EmployeesPanel.EmpoyeesAdded -= OnEmployeeAdded;
            m_taskView.EmployeesPanel.EmpoyeesRemoved -= OnEmployeeRemoved;

            m_taskView.OnComplete -= CompleteTask;

            _disposable?.Dispose();
        }

        public void OpenTask(TaskButton taskButton)
        {
            m_currentButton = taskButton;
            TaskConfig selectedTask = taskButton.Config;

            m_taskView.Open();
            m_taskView.Name = selectedTask.name;
            m_taskView.Description = selectedTask.text;
            m_taskView.SetConditionCounters(selectedTask.conditions);
            m_taskView.SetRewardCounters(selectedTask.rewards.rewardAttributes);

            m_taskModel.Init(selectedTask);
        }

        private void SubscribeTaskPanel()
        {
            m_taskView.EmployeesPanel.EmpoyeesAdded += OnEmployeeAdded;
            m_taskView.EmployeesPanel.EmpoyeesRemoved += OnEmployeeRemoved;
        }

        private void OnEmployeeAdded(EmployeeData data)
        {
            m_taskModel.AddEmployee(data);
        }

        private void OnEmployeeRemoved(EmployeeData data)
        {
            m_taskModel.RemoveEmployee(data);
        }

        private void UpdateCompleteButton(bool isEnabling)
        {
            m_taskView.SetEnablingCompleteButton(isEnabling);
        }

        private void CompleteTask()
        {
            if (m_currentButton != null)
                m_currentButton.Disable();

            m_taskView.Close();
            m_officeModel.TakeReward(m_taskModel.GetReward());
        }

        private void CreateTasks()
        {
            m_taskButtons.AddRange(m_officeView.TaskButtons);

            TurnConfig currentTurn = m_turnManager.CurrentTurn;
            List<TaskConfig> currentPossibleTasks = new List<TaskConfig>();
            currentPossibleTasks.AddRange(currentTurn.possibleTasks);

            foreach (TaskDifficulty difficulty in currentTurn.taskDifficulties)
            {
                TaskButton taskButton = m_taskButtons[Random.Range(0, m_taskButtons.Count)];
                m_taskButtons.Remove(taskButton);

                taskButton.Init(GetRandomTask(difficulty, currentPossibleTasks));

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

        [CanBeNull]
        private TaskConfig GetRandomTask(TaskDifficulty difficulty, List<TaskConfig> possibleTasks)
        {
            List<TaskConfig> filteredConfigs = possibleTasks.Where(task => task.difficulty == difficulty).ToList();
            if (filteredConfigs.Count == 0)
            {
                Debug.LogWarning($"No cards with difficulty {difficulty} found!");
                return null;
            }

            TaskConfig randomConfig = filteredConfigs[Random.Range(0, filteredConfigs.Count)];

            possibleTasks.Remove(randomConfig);
            return randomConfig;
        }
    }
}