using _Core.Scripts.DeckLogic;
using _Core.Scripts.DeckLogic.Deck;
using _Core.Scripts.DeckLogic.Hand;
using _Core.Scripts.DeckLogic.Trash;
using _Core.Scripts.Employees.View;
using _Core.Scripts.OfficeScripts;
using _Core.Scripts.OfficeScripts.View;
using _Core.Scripts.Tasks;
using _Core.Scripts.Tasks.View;
using _Core.Scripts.TurnManagerScripts;
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
        [SerializeField] private DeckView m_deckView;
        [SerializeField] private TrashView m_trashView;
        [SerializeField] private EndGameView m_gameView;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<OfficeTaskController>(Lifetime.Singleton);
            builder.Register<DeckModel>(Lifetime.Singleton);
            builder.Register<TrashModel>(Lifetime.Singleton);
            builder.Register<HandModel>(Lifetime.Singleton);
            builder.Register<DeckController>(Lifetime.Singleton);
            builder.Register<TurnManager>(Lifetime.Singleton);
            builder.Register<OfficeController>(Lifetime.Singleton);
            builder.Register<OfficeModel>(Lifetime.Singleton);
            builder.Register<TaskModel>(Lifetime.Singleton);
            builder.Register<GameEndController>(Lifetime.Singleton);

            builder.RegisterComponent(_employeesPanelView);
            builder.RegisterComponent(_taskPopup);
            builder.RegisterComponent(_officeView);
            builder.RegisterComponent(m_deckView);
            builder.RegisterComponent(m_trashView);
            builder.RegisterComponent(m_gameView);

            builder.RegisterEntryPoint<OfficeInstaller>();
        }
    }
}