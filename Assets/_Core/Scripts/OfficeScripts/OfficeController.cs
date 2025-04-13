using _Core.Scripts.OfficeScripts.View;
using _Core.Scripts.TurnManagerScripts;
using R3;

namespace _Core.Scripts.OfficeScripts
{
    public class OfficeController
    {
        private OfficeView m_officeView;
        private TurnManager m_turnManager;
        private OfficeModel m_officeModel;

        private CompositeDisposable _disposable;

        public OfficeController(OfficeView officeView, TurnManager turnManager, OfficeModel officeModel)
        {
            m_officeView = officeView;
            m_turnManager = turnManager;
            m_officeModel = officeModel;
        }

        public void Init()
        {
            _disposable = new CompositeDisposable();

            InitCounters();
            SubscribeTurnManager();

            m_officeView.EndTurnClicked += EndTurn;
        }

        public void Dispose()
        {
            _disposable.Dispose();

            m_officeView.EndTurnClicked -= EndTurn;
        }

        private void InitCounters()
        {
            m_officeView.OfficeCounters.ForEach(counter => counter.Init(m_officeModel));

            m_officeView.ExpCounter.Init(m_officeModel);
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
            m_turnManager.NextStep();
        }
    }
}