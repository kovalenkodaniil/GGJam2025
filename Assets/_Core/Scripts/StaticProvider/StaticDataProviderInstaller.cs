using _Core.Scripts.Employees;
using _Core.Scripts.Tasks;
using _Core.Scripts.TurnManagerScripts;
using UnityEngine;

namespace _Core.StaticProvider
{
    public class StaticDataProviderInstaller : MonoBehaviour
    {
        [SerializeField] private EmployeeAsset m_employeerAsset;
        [SerializeField] private TaskAsset m_taskAsset;
        [SerializeField] private TurnAsset m_turnAsset;

        public void Awake()
        {
            StaticDataProvider.Add(new EmployeeDataProvider(m_employeerAsset));
            StaticDataProvider.Add(new TaskDataProvider(m_taskAsset));
            StaticDataProvider.Add(new TurnDataProvider(m_turnAsset));
        }
    }
}