using TMPro;
using UnityEngine;

namespace _Core.Scripts.Tasks.View
{
    public class TaskView : MonoBehaviour
    {
        [SerializeField] private GameObject m_conatainer;
        [SerializeField] private TMP_Text m_name;
        [SerializeField] private TMP_Text m_description;
        [SerializeField] private TaskEmployeesPanel m_empoyeesPanel;

        public TaskEmployeesPanel EmployeesPanel => m_empoyeesPanel;

        public string Name
        {
            set => m_name.text = value;
        }
        public string Description
        {
            set => m_description.text = value;
        }

        public void Open()
        {
            m_conatainer.SetActive(true);

            m_empoyeesPanel.Init();
        }

        public void Close()
        {
            m_conatainer.SetActive(false);
        }
    }
}