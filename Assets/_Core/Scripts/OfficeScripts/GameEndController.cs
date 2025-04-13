using System.Collections.Generic;
using _Core.Scripts.OfficeScripts.View;
using _Core.Scripts.TurnManagerScripts;
using _Core.StaticProvider;
using R3;

namespace _Core.Scripts.OfficeScripts
{
    public class GameEndController
    {
        private OfficeTaskController m_officeTaskController;
        private TurnManager m_turnManager;
        private EndGameView m_endGameView;
        private OfficeModel m_officeModel;

        private CompositeDisposable m_disposable;

        public GameEndController(
            OfficeTaskController officeTaskController,
            TurnManager turnManager,
            EndGameView endGameView,
            OfficeModel officeModel)
        {
            m_officeTaskController = officeTaskController;
            m_turnManager = turnManager;
            m_endGameView = endGameView;
            m_officeModel = officeModel;
        }

        public void Init()
        {
            m_disposable = new CompositeDisposable();

            m_officeTaskController.IsAllTaskComplete
                .Subscribe(CheckEndGame)
                .AddTo(m_disposable);

            m_turnManager.IsGameOver
                .Subscribe(LoseGame)
                .AddTo(m_disposable);
        }

        private void LoseGame(bool isGameOver)
        {
            if (isGameOver)
            {
                m_endGameView.OpenLose(StaticDataProvider.Get<OfficeDataProvider>().asset.endingLose);
            }
        }

        private void CheckEndGame(bool isGameOver)
        {
            if (isGameOver)
            {
                ChooseEnding();
                m_endGameView.Open();
            }
        }

        private void ChooseEnding()
        {
            List<EndingConfig> endingConfigs = StaticDataProvider.Get<OfficeDataProvider>().asset.endingsByGameStat;

            m_officeModel._rewardAttributes.ForEach(gameStat =>
            {
                EndingConfig config = endingConfigs.Find(config => config.gameStat == gameStat.Value.type);

                if (gameStat.Value.value >= config.conditionValue)
                {
                    m_endGameView.FillEnding(gameStat.Value.type, config.moreVariant);
                }
                else
                {
                    m_endGameView.FillEnding(gameStat.Value.type, config.lessVariant);
                }
            });
        }
    }
}