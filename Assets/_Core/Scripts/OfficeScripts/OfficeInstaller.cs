using _Core.Scripts.Employees.View;
using _Core.Scripts.OfficeScripts.View;
using _Core.Scripts.Tasks.View;
using VContainer;
using VContainer.Unity;

namespace _Core.Scripts.OfficeScripts
{
    public class OfficeInstaller: IStartable
    {
        [Inject] private EmployeesPanelView m_employeesPanelView;
        [Inject] private OfficeView m_officeView;
        [Inject] private TaskView m_taskView;
        [Inject] private OfficeTaskController m_taskController;

        public void Start()
        {
            m_employeesPanelView.Init();
            m_officeView.Init();

            m_taskController.Init();
        }
    }
}