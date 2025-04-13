using _Core.Scripts.Tasks;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Core.Scripts.OfficeScripts.View
{
    public class OfficeCounterView : MonoBehaviour
    {
        [SerializeField] private TMP_Text m_count;
        [SerializeField] private Slider m_progressBar;
        [SerializeField] private EnumReward m_type;

        private CompositeDisposable m_disposable;

        public EnumReward Type => m_type;

        public void Init(OfficeModel officeModel)
        {
            m_disposable = new CompositeDisposable();

            m_progressBar.maxValue = 60;

            switch (Type)
            {
                case EnumReward.Gameplay:
                    officeModel.Gameplay.Subscribe(UpdateCounter).AddTo(m_disposable);
                    break;

                case EnumReward.Graphic:
                    officeModel.Graphic.Subscribe(UpdateCounter).AddTo(m_disposable);
                    break;

                case EnumReward.Popularity:
                    officeModel.Popularity.Subscribe(UpdateCounter).AddTo(m_disposable);
                    break;

                case EnumReward.Profit:
                    officeModel.Profit.Subscribe(UpdateCounter).AddTo(m_disposable);
                    break;

                case EnumReward.Quality:
                    officeModel.Quality.Subscribe(UpdateCounter).AddTo(m_disposable);
                    break;
            }
        }

        private void UpdateCounter(RewardAttribute rewardAttribute)
        {
            m_count.text = $"{rewardAttribute.value}";

            m_progressBar.value = rewardAttribute.value;
        }
    }
}