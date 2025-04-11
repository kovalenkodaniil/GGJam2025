using _Core.Scripts.Employees.View;
using _Core.Scripts.Tasks.View;
using R3;
using UnityEngine;

namespace _Core.Scripts.Employees
{
    public class EmployeesPresenter
    {
        #region Dependencies

        private EmployeesWidget m_view;
        private EmployeeConfig m_config;
        private TaskEmployeesPanel m_employeesTaskPanel;

        #endregion

        private CompositeDisposable m_disposable;
        private Transform m_defaultParent;
        private Transform m_dragParent;

        public EmployeesPresenter(EmployeesWidget view, EmployeeConfig config, TaskView taskView)
        {
            m_view = view;
            m_config = config;
            m_employeesTaskPanel = taskView.EmployeesPanel;

            m_disposable = new CompositeDisposable();
        }

        public void Enable()
        {
            UpdateView();
        }

        public void Disable()
        {
            m_disposable.Dispose();
        }

        private void SetDragParent()
        {
            m_view.transform.SetParent(m_dragParent);
        }

        private void CheckViewPositionAfterDrag()
        {
            Vector3 dragPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            if (m_employeesTaskPanel.IsPositionInPanel(dragPosition))
            {
                m_employeesTaskPanel.AddCharacter(m_config);
                m_view.transform.SetParent(m_defaultParent);
            }
            else
            {
                m_view.transform.SetParent(m_defaultParent);
            }
        }

        private void UpdateView()
        {
            m_view.SetCharacterState();
            m_view.Portrait = m_config.icon;

            for (int i = 0; i < m_config.characterictics.Count; i++)
            {
                m_view.Counters[i].text = m_config.characterictics[i].value.ToString();
            }
        }
    }
}