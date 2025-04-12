using _Core.Scripts.OfficeScripts.View;
using _Core.Scripts.TurnManagerScripts;
using R3;

namespace _Core.Scripts.OfficeScripts
{
    public class GameEndController
    {
        private OfficeTaskController m_officeTaskController;
        private TurnManager m_turnManager;
        private EndGameView m_endGameView;

        private CompositeDisposable m_disposable;

        public GameEndController(
            OfficeTaskController officeTaskController,
            TurnManager turnManager,
            EndGameView endGameView)
        {
            m_officeTaskController = officeTaskController;
            m_turnManager = turnManager;
            m_endGameView = endGameView;
        }

        public void Init()
        {
            m_disposable = new CompositeDisposable();

            m_officeTaskController.IsAllTaskComplete
                .Subscribe(CheckEndGame)
                .AddTo(m_disposable);

            m_turnManager.IsGameOver
                .Subscribe(CheckEndGame)
                .AddTo(m_disposable);
        }

        private void CheckEndGame(bool isGameOver)
        {
            if (isGameOver)
            {
                m_endGameView.Open();
            }
        }
    }
}