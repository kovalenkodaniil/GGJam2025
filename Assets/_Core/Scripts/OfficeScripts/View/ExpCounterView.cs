using System;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Core.Scripts.OfficeScripts.View
{
    public class ExpCounterView : MonoBehaviour
    {
        [SerializeField] private TMP_Text m_count;
        [SerializeField] private Slider m_progressBar;

        private CompositeDisposable m_disposable;
        private OfficeModel m_officeModel;

        public void Init(OfficeModel officeModel)
        {
            m_disposable = new CompositeDisposable();
            m_officeModel = officeModel;

            Reset();

            m_officeModel.Experience
                .Subscribe(UpdateTimer)
                .AddTo(m_disposable);

            m_officeModel.LevelUped += Reset;
        }

        public void OnDisable()
        {
            m_disposable?.Dispose();

            m_officeModel.LevelUped -= Reset;
        }

        public void Reset()
        {
            OfficeConfig currentOfficeConfig = m_officeModel.GetOfficeConfig(m_officeModel._currentLevel);

            m_progressBar.maxValue = currentOfficeConfig.experience;

            UpdateTimer(m_officeModel.Experience.Value);
        }

        private void UpdateTimer(int count)
        {
            m_count.text = $"{count}/{m_progressBar.maxValue}";

            m_progressBar.value = count;
        }
    }
}