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

        private List<TaskButton> m_taskButtons;

        private TaskConfig _taskConfig;
        private CompositeDisposable _disposable;
        private TaskButton m_currentButton;

        private List<TaskData> m_taskData;
        public ReactiveProperty<bool> IsAllTaskComplete = new (false);

        public OfficeTaskController()
        {
            _disposable = new CompositeDisposable();

            m_taskButtons = new List<TaskButton>();
            m_taskData = new List<TaskData>();
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
            TaskData selectedTask = taskButton.Data;

            m_taskView.Open();
            m_taskView.Name = selectedTask.Config.name;
            m_taskView.Description = selectedTask.Config.text;
            m_taskView.Comment = selectedTask.Config.comment;
            m_taskView.SetConditionCounters(selectedTask.Config.conditions);
            m_taskView.SetRewardCounters(selectedTask.Config.rewards);

            m_taskModel.Init(selectedTask);
            m_taskView.SetCurrentCounters(m_taskModel.CurrentConditions);
        }

        private void SubscribeTaskPanel()
        {
            m_taskView.EmployeesPanel.EmpoyeesAdded += OnEmployeeAdded;
            m_taskView.EmployeesPanel.EmpoyeesRemoved += OnEmployeeRemoved;
        }

        private void OnEmployeeAdded(EmployeeData data)
        {
            m_taskModel.AddEmployee(data);

            m_taskView.SetCurrentCounters(m_taskModel.CurrentConditions);
        }

        private void OnEmployeeRemoved(EmployeeData data)
        {
            m_taskModel.RemoveEmployee(data);

            m_taskView.SetCurrentCounters(m_taskModel.CurrentConditions);
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
            m_officeModel.TakeReward(m_taskModel.GetReward(), m_taskModel.Task.Config.difficultyConfig.experience);
            m_taskModel.Task.IsCompleted = new ReactiveProperty<bool>(true);

            CheckCompletedTasks();
        }

        private void CheckCompletedTasks()
        {
            foreach (TaskData task in m_taskData)
            {
                if (task.IsCompleted.Value != true)
                {
                    return;
                }
            }

            IsAllTaskComplete = new ReactiveProperty<bool>(true);
        }

        private void CreateTasks()
        {
            m_taskButtons.AddRange(m_officeView.TaskButtons);
            List<TaskButton> tempButtons = new List<TaskButton>();
            tempButtons.AddRange(m_officeView.TaskButtons);

            m_taskData.Clear();

            TurnConfig currentTurn = m_turnManager.CurrentTurn;
            List<TaskData> currentPossibleTasks = new List<TaskData>();

            currentTurn.possibleTasks.ForEach(config => currentPossibleTasks.Add(new TaskData(config)));

            foreach (TaskDifficulty difficulty in currentTurn.taskDifficulties)
            {
                TaskButton taskButton = tempButtons[Random.Range(0, tempButtons.Count)];
                tempButtons.Remove(taskButton);

                TaskData task = GetRandomTask(difficulty, currentPossibleTasks);

                m_taskData.Add(task);
                taskButton.Init(task);
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

            m_taskButtons.Clear();
        }

        [CanBeNull]
        private TaskData GetRandomTask(TaskDifficulty difficulty, List<TaskData> possibleTasks)
        {
            List<TaskData> filteredConfigs = possibleTasks.Where(task => task.Config.difficultyConfig.difficulty == difficulty).ToList();
            if (filteredConfigs.Count == 0)
            {
                Debug.LogWarning($"No cards with difficulty {difficulty} found!");
                return null;
            }

            TaskData randomConfig = filteredConfigs[Random.Range(0, filteredConfigs.Count)];

            possibleTasks.Remove(randomConfig);
            return randomConfig;
        }
    }
}