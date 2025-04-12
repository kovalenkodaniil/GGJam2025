using System.Collections.Generic;
using _Core.Scripts.DeckLogic.Hand;
using _Core.Scripts.Tasks.View;
using _Core.StaticProvider;
using UnityEngine;
using VContainer;

namespace _Core.Scripts.Employees.View
{
    public class EmployeesPanelView : MonoBehaviour
    {
        [Inject] private TaskView m_taskView;
        [Inject] private HandModel m_handModel;

        [SerializeField] private Transform m_employeesParent;
        [SerializeField] private EmployeesWidget m_employeesViewPrefab;

        private List<EmployeesPresenter> m_presenters;

        public void Init()
        {
            m_presenters = new List<EmployeesPresenter>();

            m_handModel.EmployeesAdded += CreateEmployee;
            m_handModel.EmployeesRemoved += DestroyEmployee;

            EnablePresenters();
        }

        private void OnDestroy()
        {
            m_presenters.ForEach(presenter => presenter.Disable());

            m_handModel.EmployeesAdded -= CreateEmployee;
            m_handModel.EmployeesRemoved -= DestroyEmployee;
        }

        private void EnablePresenters()
        {
            m_presenters.ForEach(presenter => presenter.Enable());
        }

        private void CreateEmployee(EmployeeData employeeData)
        {
            EmployeesWidget view = Instantiate(m_employeesViewPrefab, m_employeesParent);

            EmployeesPresenter newPresenter = new EmployeesPresenter(view, employeeData.Config, m_taskView);

            newPresenter.Enable();
            m_presenters.Add(newPresenter);
        }

        private void DestroyEmployee(EmployeeData employeeData)
        {
            EmployeesPresenter removedPresenter = m_presenters.Find(presenter => presenter.Config.id == employeeData.Config.id);

            removedPresenter.Disable();

            m_presenters.Remove(removedPresenter);
        }
    }
}