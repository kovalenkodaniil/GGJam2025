using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Core.Scripts.Tasks.View
{
    public class TaskButton : MonoBehaviour
    {
        [SerializeField] private Button m_button;

        private TaskConfig m_config;

        public event Action<TaskConfig> TaskSelected;

        public void Init(TaskConfig config)
        {
            m_config = config;

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
            TaskSelected?.Invoke(m_config);
        }
    }
}