using System.Collections.Generic;
using _Core.Scripts.DeckLogic;
using _Core.Scripts.DeckLogic.Deck;
using _Core.Scripts.DeckLogic.Hand;
using _Core.Scripts.DeckLogic.Trash;
using _Core.Scripts.Employees;
using _Core.Scripts.Employees.View;
using _Core.Scripts.OfficeScripts.View;
using _Core.Scripts.Tasks.View;
using _Core.Scripts.TurnManagerScripts;
using _Core.StaticProvider;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Core.Scripts.OfficeScripts
{
    public class OfficeInstaller: IStartable
    {
        [Inject] private EmployeesPanelView m_employeesPanelView;
        [Inject] private OfficeView m_officeView;
        [Inject] private TaskView m_taskView;
        [Inject] private OfficeTaskController m_taskController;
        [Inject] private DeckModel m_deck;
        [Inject] private TrashModel m_trash;
        [Inject] private HandModel m_hand;
        [Inject] private TurnManager m_turnManager;
        [Inject] private DeckController m_deckController;
        [Inject] private DeckView m_deckView;
        [Inject] private TrashView m_trashView;
        [Inject] private OfficeController m_officeController;
        [Inject] private GameEndController m_gameEndController;

        public void Start()
        {
            m_employeesPanelView.Init();
            m_officeView.Init();

            CreateDeck();

            m_gameEndController.Init();
            m_taskController.Init();
            m_officeController.Init();
            m_deckController.Init();
            m_turnManager.Init();

            m_deckView.Init();
            m_trashView.Init();
        }

        private void CreateDeck()
        {
            List<EmployeeData> employees = new List<EmployeeData>();

            StaticDataProvider.Get<EmployeeDataProvider>().asset.characters.ForEach(character =>
            {
                employees.Add(new EmployeeData(character));
            });

            m_deck.Init(employees);
        }
    }
}