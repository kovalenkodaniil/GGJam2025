using System;
using _Core.Scripts.Employees.View;
using _Core.Scripts.Tasks.View;
using R3;
using UnityEngine;
using Object = UnityEngine.Object;

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

        private bool isInTask;

        public event Action<EmployeesPresenter> OnDiscarded;

        public EmployeeConfig Config => m_config;

        public EmployeesPresenter(EmployeesWidget view, EmployeeData data, TaskView taskView, Transform defaultParent)
        {
            m_view = view;
            m_data = data;
            m_config = data.Config;
            m_employeesTaskPanel = taskView.EmployeesPanel;

            m_dragParent = defaultParent.parent;
            m_defaultParent = defaultParent;

            m_disposable = new CompositeDisposable();

            isInTask = false;
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
            if (m_view == null)
                return;

            m_disposable.Dispose();
            m_view.OnDiscarded -= TryDiscarded;

            Object.Destroy(m_view.gameObject);
        }

        public void PlayDiscardAnimation(Vector3 discardPosition)
        {
            if (m_view == null)
            {
                OnDiscarded?.Invoke(this);
                return;
            }

            m_view.OnDiscarded += TryDiscarded;
            m_view.PlayDiscardAnimation(discardPosition);
        }

        private void TryDiscarded()
        {
            OnDiscarded?.Invoke(this);
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
                AddOnTaskPanel();
            }
            else
            {
                ReturnOnPanel();
            }
        }

        private void AddOnTaskPanel()
        {
            m_employeesTaskPanel.AddEmployee(m_data, m_view);
            isInTask = true;

            m_employeesTaskPanel.EmpoyeesTrashed += Disable;
            m_employeesTaskPanel.EmpoyeesReturn += ReturnOnPanel;
        }

        private void ReturnOnPanel()
        {
            isInTask = false;
            m_employeesTaskPanel.RemoveEmployee(m_data);
            m_view.transform.SetParent(m_defaultParent);
            m_view.transform.localPosition = Vector3.zero;

            m_employeesTaskPanel.EmpoyeesReturn -= ReturnOnPanel;
            m_employeesTaskPanel.EmpoyeesTrashed -= Disable;
        }

        private void UpdateView()
        {
            m_view.SetCharacterState();
            m_view.Portrait = m_config.icon;
            m_view.Name = m_config.name;

            m_view.SetConditionCounters(m_config.characterictics);
        }
    }
}