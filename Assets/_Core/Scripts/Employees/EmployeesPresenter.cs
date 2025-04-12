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

        private EmployeeData m_data;
        private CompositeDisposable m_disposable;
        private Transform m_defaultParent;
        private Transform m_dragParent;

        public EmployeeConfig Config => m_config;

        public EmployeesPresenter(EmployeesWidget view, EmployeeData data, TaskView taskView)
        {
            m_view = view;
            m_data = data;
            m_config = data.Config;
            m_employeesTaskPanel = taskView.EmployeesPanel;

            m_dragParent = taskView.EmployeesPanel.transform;
            m_defaultParent = m_view.transform.parent;

            m_disposable = new CompositeDisposable();
        }

        public void Enable()
        {
            UpdateView();

            m_view.OnDragEnded
                .Subscribe(_ => CheckViewPositionAfterDrag())
                .AddTo(m_disposable);

            m_view.OnDragHasBegun
                .Subscribe(_ => SetDragParent())
                .AddTo(m_disposable);
        }

        public void Disable()
        {
            m_disposable.Dispose();

            Object.Destroy(m_view.gameObject);
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
                m_employeesTaskPanel.AddEmployee(m_data);
                m_view.gameObject.SetActive(false);
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
            m_view.Name = m_config.name;

            for (int i = 0; i < m_config.characterictics.Count; i++)
            {
                m_view.Counters[i].text = m_config.characterictics[i].value.ToString();
            }
        }
    }
}