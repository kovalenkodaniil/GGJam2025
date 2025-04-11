using System;
using _Core.Scripts.Tasks.View;
using UnityEngine;
using UnityEngine.UI;
using VContainer;

namespace _Core.Scripts.OfficeScripts.View
{
    public class OfficeView : MonoBehaviour
    {
        [SerializeField] private Button m_task_button;

        public event Action TackClicked;

        public void Init()
        {
            m_task_button.onClick.AddListener(OpenTask);
        }

        public void OnDisable()
        {
            m_task_button.onClick.RemoveListener(OpenTask);
        }

        private void OpenTask()
        {
            TackClicked?.Invoke();
        }
    }
}