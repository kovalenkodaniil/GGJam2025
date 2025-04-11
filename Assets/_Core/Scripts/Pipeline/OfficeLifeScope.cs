using _Core.Scripts.Employees.View;
using _Core.Scripts.OfficeScripts;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Core.Scripts.Pipeline
{
    public class OfficeLifeScope: LifetimeScope
    {
        [SerializeField] private EmployeesPanelView _employeesPanelView;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(_employeesPanelView);

            builder.RegisterEntryPoint<OfficeInstaller>();
        }
    }
}