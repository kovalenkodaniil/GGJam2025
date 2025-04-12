using _Core.Scripts.Employees;
using TMPro;
using UnityEngine;

namespace _Core.Scripts.Tasks.View
{
    public class TaskConditionView : MonoBehaviour
    {
        [SerializeField] private TMP_Text m_count;
        [SerializeField] private EnumСharacteristic m_type;

        public EnumСharacteristic Type => m_type;

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