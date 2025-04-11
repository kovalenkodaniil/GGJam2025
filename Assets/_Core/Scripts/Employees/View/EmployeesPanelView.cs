using System.Collections.Generic;
using _Core.Scripts.Tasks.View;
using _Core.StaticProvider;
using UnityEngine;
using VContainer;

namespace _Core.Scripts.Employees.View
{
    public class EmployeesPanelView : MonoBehaviour
    {
        [Inject] private TaskView m_taskView;

        [SerializeField] private Transform m_employeesParent;
        [SerializeField] private EmployeesWidget m_employeesViewPrefab;

        private List<EmployeesPresenter> m_presenters;

        public void Init()
        {
            CreateCharacters();
            EnablePresenters();
        }

        private void OnDestroy()
        {
            m_presenters.ForEach(presenter => presenter.Disable());
        }

        private void EnablePresenters()
        {
            m_presenters.ForEach(presenter => presenter.Enable());
        }

        private void CreateCharacters()
        {
            List<EmployeeConfig> employeeConfigs = StaticDataProvider.Get<EmployeeDataProvider>().asset.characters;
            m_presenters = new List<EmployeesPresenter>();

            foreach (var config in employeeConfigs)
            {
                EmployeesWidget view = Instantiate(m_employeesViewPrefab, m_employeesParent);

                m_presenters.Add(new EmployeesPresenter(view, config, m_taskView));
            }
        }
    }
}