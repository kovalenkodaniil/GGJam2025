using System;
using R3;
using TMPro;
using UnityEngine;

namespace _Core.Scripts.DeckLogic.Deck
{
    public class DeckView : MonoBehaviour
    {
        [SerializeField] private TMP_Text m_count;

        private CompositeDisposable m_disposable;

        public void Init(DeckModel deckModel)
        {
            m_disposable = new CompositeDisposable();

            deckModel.CardCount
                .Subscribe(UpdateCount)
                .AddTo(m_disposable);
        }

        public void OnDisable()
        {
            m_disposable?.Dispose();
        }

        private void UpdateCount(int count)
        {
            m_count.text = $"{count}";
        }
    }
}