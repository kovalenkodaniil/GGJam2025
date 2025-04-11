using _Core.StaticProvider;

namespace _Core.Scripts.Employees
{
    public class EmployeeDataProvider: IStaticDataProvider
    {
        public EmployeeAsset asset;

        public EmployeeDataProvider(EmployeeAsset asset)
        {
            this.asset = asset;
        }
    }
}