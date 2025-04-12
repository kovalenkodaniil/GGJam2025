using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Core.Scripts.Tasks.View
{
    public class TaskButton : MonoBehaviour
    {
        [SerializeField] private Button m_button;

        private TaskConfig m_config;

        public TaskConfig Config => m_config;

        public event Action<TaskButton> TaskSelected;

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
            TaskSelected?.Invoke(this);
        }
    }
}