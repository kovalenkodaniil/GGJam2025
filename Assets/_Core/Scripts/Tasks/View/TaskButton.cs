using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Core.Scripts.Tasks.View
{
    public class TaskButton : MonoBehaviour
    {
        [SerializeField] private Button m_button;

        private TaskData m_data;

        public TaskData Data => m_data;

        public event Action<TaskButton> TaskSelected;

        public void Init(TaskData data)
        {
            m_data = data;

            gameObject.SetActive(true);

            m_button.onClick.AddListener(SelectTask);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
            m_button.onClick.RemoveListener(SelectTask);
        }

        private void SelectTask()
        {
            TaskSelected?.Invoke(this);
        }
    }
}