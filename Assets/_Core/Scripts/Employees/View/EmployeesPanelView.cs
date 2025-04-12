using System.Collections.Generic;
using _Core.Scripts.DeckLogic.Deck;
using _Core.Scripts.DeckLogic.Hand;
using _Core.Scripts.DeckLogic.Trash;
using _Core.Scripts.Tasks.View;
using _Core.Scripts.TurnManagerScripts;
using R3;
using UnityEngine;
using VContainer;

namespace _Core.Scripts.Employees.View
{
    public class EmployeesPanelView : MonoBehaviour
    {
        [Inject] private TaskView m_taskView;
        [Inject] private HandModel m_handModel;
        [Inject] private TurnManager m_turnManager;
        [Inject] private DeckView m_deckView;
        [Inject] private TrashView m_trashView;

        [SerializeField] private Transform m_employeesParent;
        [SerializeField] private EmployeesWidget m_employeesViewPrefab;
        [SerializeField] private List<EmployeeSlotView> m_slotViews;

        private List<EmployeesPresenter> m_presenters;
        private CompositeDisposable m_disposable;

        public void Init()
        {
            m_presenters = new List<EmployeesPresenter>();
            m_disposable = new CompositeDisposable();

            ResetSlots();

            m_handModel.EmployeesAdded += CreateEmployee;
            m_handModel.EmployeesRemoved += DestroyEmployee;

            EnablePresenters();

            m_turnManager.OnTurnEnded
                .Subscribe(_ => ResetSlots())
                .AddTo(m_disposable);
        }

        private void OnDestroy()
        {
            m_disposable?.Dispose();

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

            EmployeeSlotView slotView = m_slotViews.Find(slot => slot.IsEmpty);
            slotView.IsEmpty = false;

            view.PlayDrawAnimation(m_deckView.transform, slotView.transform.position, slotView.transform, 0.2f);

            EmployeesPresenter newPresenter = new EmployeesPresenter(view, employeeData, m_taskView, slotView.transform);

            newPresenter.Enable();
            m_presenters.Add(newPresenter);
        }

        private void DestroyEmployee(EmployeeData employeeData)
        {
            EmployeesPresenter removedPresenter = m_presenters.Find(presenter => presenter.Config.id == employeeData.Config.id);

            removedPresenter.OnDiscarded += DestroyDicarded;
            removedPresenter.PlayDiscardAnimation(m_trashView.transform.position);
        }

        private void DestroyDicarded(EmployeesPresenter discardedPresenter)
        {
            discardedPresenter.OnDiscarded -= DestroyDicarded;

            discardedPresenter.Disable();

            m_presenters.Remove(discardedPresenter);
        }

        private void ResetSlots()
        {
            m_slotViews.ForEach(view => view.IsEmpty = true);
        }
    }
}