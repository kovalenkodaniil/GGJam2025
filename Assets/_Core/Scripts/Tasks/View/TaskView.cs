using System;
using System.Collections.Generic;
using _Core.Scripts.Employees;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Core.Scripts.Tasks.View
{
    public class TaskView : MonoBehaviour
    {
        [SerializeField] private GameObject m_conatainer;
        [SerializeField] private TMP_Text m_name;
        [SerializeField] private TMP_Text m_description;
        [SerializeField] private TaskEmployeesPanel m_empoyeesPanel;
        [SerializeField] private Button m_completeButton;
        [SerializeField] private List<TaskConditionView> m_conditionView;
        [SerializeField] private List<TaskRewardView> m_rewardView;

        public event Action OnComplete;

        public TaskEmployeesPanel EmployeesPanel => m_empoyeesPanel;

        public string Name
        {
            set => m_name.text = value;
        }
        public string Description
        {
            set => m_description.text = value;
        }

        public void Open()
        {
            m_conatainer.SetActive(true);

            m_empoyeesPanel.Init();

            m_completeButton.onClick.AddListener(CompleteTask);
        }

        public void Close()
        {
            m_conatainer.SetActive(false);

            m_completeButton.onClick.RemoveListener(CompleteTask);

            m_empoyeesPanel.ReturnEmployees();
        }

        public void SetEnablingCompleteButton(bool isEnabling)
        {
            m_completeButton.interactable = isEnabling;
        }

        public void SetConditionCounters(List<CharacterAttribute> attributes)
        {
            m_conditionView.ForEach(view => view.SetCount(0));

            attributes.ForEach(attribute =>
            {
                m_conditionView.Find(view => view.Type == attribute.type).SetCount(attribute.value);
            });
        }

        public void SetRewardCounters(List<RewardAttribute> rewards)
        {
            m_rewardView.ForEach(view => view.SetCount(0));

            rewards.ForEach(attribute =>
            {
                m_rewardView.Find(view => view.Type == attribute.type).SetCount(attribute.value);
            });
        }

        private void CompleteTask()
        {
            OnComplete?.Invoke();

            m_empoyeesPanel.TrashEmployee();
        }
    }
}