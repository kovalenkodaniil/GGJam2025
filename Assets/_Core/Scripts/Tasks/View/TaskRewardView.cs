using _Core.Scripts.Employees;
using TMPro;
using UnityEngine;

namespace _Core.Scripts.Tasks.View
{
    public class TaskRewardView : MonoBehaviour
    {
        [SerializeField] private TMP_Text m_count;
        [SerializeField] private EnumReward m_type;

        public EnumReward Type => m_type;

        public void SetCount(int count)
        {
            m_count.text = $"{count}";

            gameObject.SetActive(true);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}