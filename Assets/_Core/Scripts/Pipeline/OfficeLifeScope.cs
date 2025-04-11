using _Core.Scripts.DeckLogic.Deck;
using _Core.Scripts.DeckLogic.Hand;
using _Core.Scripts.DeckLogic.Trash;
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
            builder.Register<DeckModel>(Lifetime.Singleton);
            builder.Register<TrashModel>(Lifetime.Singleton);
            builder.Register<HandModel>(Lifetime.Singleton);

            builder.RegisterComponent(_employeesPanelView);
            builder.RegisterComponent(_taskPopup);
            builder.RegisterComponent(_officeView);

            builder.RegisterEntryPoint<OfficeInstaller>();
        }
    }
}