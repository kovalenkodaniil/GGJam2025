using System.Collections.Generic;
using _Core.StaticProvider;
using UnityEngine;

namespace _Core.Scripts.OfficeScripts.View
{
    public class HelpView : MonoBehaviour
    {
        [SerializeField] private GameObject m_conatainer;
        [SerializeField] private List<GameObject> m_slides;

        private int m_currentIndex;

        public void Open()
        {
            m_conatainer.SetActive(true);

            ShowSlide(0);
        }

        public void Close()
        {
            PlayClick();
            m_conatainer.SetActive(false);
        }

        public void ShowSlide(int slideCount)
        {
            m_slides.ForEach(slide => slide.SetActive(false));

            m_slides[slideCount].SetActive(true);
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
        }

        private void PlayClick()
        {
            SoundManager.Instance.PlaySfx(StaticDataProvider.Get<SoundDataProvider>().asset.buttonClick);
        }
    }
}