using System;
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
        }

        private void CompleteTask()
        {
            OnComplete?.Invoke();
        }
    }
}