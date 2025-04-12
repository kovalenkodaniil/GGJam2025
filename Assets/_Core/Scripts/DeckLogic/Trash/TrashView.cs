using R3;
using TMPro;
using UnityEngine;
using VContainer;

namespace _Core.Scripts.DeckLogic.Trash
{
    public class TrashView : MonoBehaviour
    {
        [Inject] private TrashModel m_trashModel;

        [SerializeField] private TMP_Text m_count;

        private CompositeDisposable m_disposable;

        public void Init()
        {
            m_disposable = new CompositeDisposable();

            m_trashModel.CardCount
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