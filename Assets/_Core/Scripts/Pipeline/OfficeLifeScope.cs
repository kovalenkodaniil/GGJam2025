using _Core.Scripts.Employees.View;
using _Core.Scripts.OfficeScripts;
using _Core.Scripts.OfficeScripts.View;
using _Core.Scripts.Tasks.View;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Core.Scripts.Pipeline
{
    public class OfficeLifeScope: LifetimeScope
    {
        [SerializeField] private EmployeesPanelView _employeesPanelView;
        [SerializeField] private TaskView _taskPopup;
        [SerializeField] private OfficeView _officeView;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<OfficeTaskController>(Lifetime.Singleton);

            builder.RegisterComponent(_employeesPanelView);
            builder.RegisterComponent(_taskPopup);
            builder.RegisterComponent(_officeView);

            builder.RegisterEntryPoint<OfficeInstaller>();
        }
    }
}