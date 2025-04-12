using _Core.Scripts.DeckLogic.Deck;
using _Core.Scripts.DeckLogic.Hand;
using _Core.Scripts.DeckLogic.Trash;
using _Core.Scripts.TurnManagerScripts;
using R3;
using VContainer;

namespace _Core.Scripts.DeckLogic
{
    public class DeckController
    {
        private DeckModel m_deck;
        private TrashModel m_trash;
        private HandModel m_hand;
        private TurnManager m_turnManager;

        private CompositeDisposable _disposable;

        public DeckController(
            DeckModel deck,
            TrashModel trash,
            HandModel hand,
            TurnManager turnManager)
        {
            m_deck = deck;
            m_trash = trash;
            m_hand = hand;
            m_turnManager = turnManager;

            _disposable = new CompositeDisposable();
        }

        public void Init()
        {
            SubscribeTurnManager();
        }

        public void Dispose() => _disposable.Dispose();

        private void SubscribeTurnManager()
        {
            m_turnManager.OnTurnStarted
                .Subscribe(_ =>
                {
                    DrawNewHand();
                    m_turnManager.NextStep();
                })
                .AddTo(_disposable);

            m_turnManager.OnTurnEnded
                .Subscribe(_ =>
                {
                    DiscardHand();
                    m_turnManager.NextStep();
                })
                .AddTo(_disposable);
        }

        private void DrawNewHand()
        {
            m_deck.GetCards(3).ForEach(card =>
            {
                m_hand.AddCard(card);
            });
        }

        private void DiscardHand()
        {
            m_hand.DiscardHand();
        }
    }
}