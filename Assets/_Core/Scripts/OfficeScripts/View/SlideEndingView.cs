using _Core.Scripts.Tasks;
using TMPro;
using UnityEngine;

namespace _Core.Scripts.OfficeScripts.View
{
    public class SlideEndingView : MonoBehaviour
    {
        [SerializeField] private GameObject m_container;
        [SerializeField] private TMP_Text m_endingText;
        [SerializeField] private EnumReward m_type;

        public EnumReward Type => m_type;

        public void SetEnding(string ending)
        {
            m_endingText.text = ending;
        }

        public void Show()
        {
            m_container.SetActive(true);
        }

        public void Hide()
        {
            m_container.SetActive(false);
        }
    }
}