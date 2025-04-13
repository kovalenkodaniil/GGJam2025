using System.Collections.Generic;
using _Core.Scripts.MainMenuScripts;
using _Core.Scripts.Tasks;
using _Core.StaticProvider;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;

namespace _Core.Scripts.OfficeScripts.View
{
    public class EndGameView : MonoBehaviour
    {
        [Inject] private MainMenu m_menu;

        [SerializeField] private GameObject m_conatainer;
        [SerializeField] private GameObject m_nextButtom;
        [SerializeField] private GameObject m_prevButtom;
        [SerializeField] private GameObject m_loseContainer;
        [SerializeField] private TMP_Text m_loseEnding;
        [SerializeField] private List<SlideEndingView> m_slides;

        private int m_currentIndex;

        public void Open()
        {
            m_conatainer.SetActive(true);

            m_loseContainer.SetActive(false);
            EnablingButton();
            ShowSlide(0);
        }

        public void OpenLose(string loseEnding)
        {
            m_conatainer.SetActive(true);

            m_nextButtom.SetActive(false);
            m_prevButtom.SetActive(false);

            m_loseContainer.SetActive(true);
            m_loseEnding.text = loseEnding;
        }

        public void Close()
        {
            m_conatainer.SetActive(false);
        }

        public void ShowSlide(int slideCount)
        {
            m_slides.ForEach(slide => slide.Hide());

            m_slides[slideCount].Show();
        }

        public void NextPage()
        {
            m_currentIndex++;

            if (m_currentIndex >= m_slides.Count - 1)
            {
                m_currentIndex = m_slides.Count - 1;
            }

            PlayClick();
            ShowSlide(m_currentIndex);

            EnablingButton();
        }

        public void PrevPage()
        {
            m_currentIndex--;

            if (m_currentIndex <= 0)
            {
                m_currentIndex = 0;
            }

            PlayClick();
            ShowSlide(m_currentIndex);

            EnablingButton();
        }

        public void FillEnding(EnumReward statType, string ending)
        {
            m_slides.Find(slide => slide.Type == statType).SetEnding(ending);
        }

        public void BackToMenu()
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("MainMenu"));
            SceneManager.UnloadSceneAsync("Office");

            m_menu.Open();
        }

        private void PlayClick()
        {
            SoundManager.Instance.PlaySfx(StaticDataProvider.Get<SoundDataProvider>().asset.buttonClick);
        }

        private void EnablingButton()
        {
            m_nextButtom.SetActive(m_currentIndex != m_slides.Count - 1);
            m_prevButtom.SetActive(m_currentIndex != 0);
        }
    }
}