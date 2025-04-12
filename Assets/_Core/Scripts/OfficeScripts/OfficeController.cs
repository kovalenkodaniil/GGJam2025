using _Core.Scripts.OfficeScripts.View;
using _Core.Scripts.TurnManagerScripts;
using R3;
using UnityEngine;

namespace _Core.Scripts.OfficeScripts
{
    public class OfficeController
    {
        private OfficeView m_officeView;
        private TurnManager m_turnManager;

        private CompositeDisposable _disposable;

        public OfficeController(OfficeView officeView, TurnManager turnManager)
        {
            m_officeView = officeView;
            m_turnManager = turnManager;
        }

        public void Init()
        {
            _disposable = new CompositeDisposable();

            SubscribeTurnManager();

            m_officeView.EndTurnClicked += EndTurn;
        }

        public void Dispose()
        {
            _disposable.Dispose();

            m_officeView.EndTurnClicked -= EndTurn;
        }

        private void SubscribeTurnManager()
        {
            m_turnManager.OnPlayerActionStarted
                .Subscribe(_ =>
                {
                    m_officeView.SetEnableTurnButton(true);
                })
                .AddTo(_disposable);

            m_turnManager.OnTurnEnded
                .Subscribe(_ =>
                {
                    m_officeView.SetEnableTurnButton(false);
                })
                .AddTo(_disposable);
        }

        private void EndTurn()
        {
            Debug.Log($"EndTurn");
            m_turnManager.NextStep();
        }
    }
}