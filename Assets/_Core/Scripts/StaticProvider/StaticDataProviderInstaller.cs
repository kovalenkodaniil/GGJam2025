using _Core.Scripts.Employees;
using UnityEngine;

namespace _Core.StaticProvider
{
    public class StaticDataProviderInstaller : MonoBehaviour
    {
        [SerializeField] private EmployeeAsset m_employeerAsset;

        public void Awake()
        {
            StaticDataProvider.Add(new EmployeeDataProvider(m_employeerAsset));
        }
    }
}