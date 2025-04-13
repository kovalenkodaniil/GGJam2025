using System;
using UnityEngine;
using UnityEngine.UI;

namespace _Core.Scripts.OfficeScripts.View
{
    public class SettingView : MonoBehaviour
    {
        [SerializeField] private GameObject m_conteiner;
        [SerializeField] private Slider m_soundSlider;
        [SerializeField] private Slider m_musicSlider;

        public void Open()
        {
            m_conteiner.SetActive(true);

            SetupSlider();
        }

        public void Close()
        {
            m_conteiner.SetActive(false);
        }

        private void SetupSlider()
        {
            m_musicSlider.maxValue = 1;
            m_musicSlider.value = SoundManager.Instance.musicVolume;

            m_soundSlider.maxValue = 1;
            m_soundSlider.value = SoundManager.Instance.sfxVolume;

            m_musicSlider.onValueChanged.AddListener(ChangeMusic);
            m_soundSlider.onValueChanged.AddListener(ChangeSound);
        }

        private void OnDisable()
        {
            Reset();
        }

        private void ChangeMusic(float value)
        {
            SoundManager.Instance.SetMusicVolume(value);
        }

        private void ChangeSound(float value)
        {
            SoundManager.Instance.SetSfxVolume(value);
        }

        private void Reset()
        {
            m_musicSlider.onValueChanged.RemoveListener(ChangeMusic);
            m_soundSlider.onValueChanged.RemoveListener(ChangeSound);
        }
    }
}