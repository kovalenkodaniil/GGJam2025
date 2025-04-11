using _Core.Scripts.Employees.View;
using R3;

namespace _Core.Scripts.Employees
{
    public class EmployeesPresenter
    {
        #region Dependencies

        private EmployeesWidget m_view;
        private EmployeeConfig m_config;

        #endregion

        private CompositeDisposable m_disposable;

        public EmployeesPresenter(EmployeesWidget view, EmployeeConfig config)
        {
            m_view = view;
            m_config = config;

            m_disposable = new CompositeDisposable();
        }

        public void Enable()
        {
            UpdateView();
        }

        public void Disable()
        {
            m_disposable.Dispose();
        }

        private void UpdateView()
        {
            m_view.SetCharacterState();
            m_view.Portrait = m_config.icon;

            for (int i = 0; i < m_config.characterictics.Count; i++)
            {
                m_view.Counters[i].text = m_config.characterictics[i].value.ToString();
            }
        }
    }
}