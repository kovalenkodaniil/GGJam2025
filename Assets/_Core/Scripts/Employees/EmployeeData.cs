using System.Collections.Generic;

namespace _Core.Scripts.Employees
{
    public class EmployeeData
    {
        public EmployeeConfig Config;
        public List<CharacterAttribute> Characteristics;

        public EmployeeData(EmployeeConfig config)
        {
            this.Config = config;

            Characteristics = new List<CharacterAttribute>();
            Characteristics.AddRange(config.characterictics);
        }
    }
}