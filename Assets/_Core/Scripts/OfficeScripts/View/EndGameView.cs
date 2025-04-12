using UnityEngine;

namespace _Core.Scripts.OfficeScripts.View
{
    public class EndGameView : MonoBehaviour
    {
        [SerializeField] private GameObject m_conatainer;

        public void Open()
        {
            m_conatainer.SetActive(true);
        }

        public void Close()
        {
            m_conatainer.SetActive(false);
        }
    }
}