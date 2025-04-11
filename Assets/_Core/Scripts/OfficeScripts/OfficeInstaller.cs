using _Core.Scripts.Employees.View;
using VContainer;
using VContainer.Unity;

namespace _Core.Scripts.OfficeScripts
{
    public class OfficeInstaller: IStartable
    {
        [Inject] private EmployeesPanelView m_employeesPanelView;

        public void Start()
        {
            m_employeesPanelView.Init();
        }
    }
}