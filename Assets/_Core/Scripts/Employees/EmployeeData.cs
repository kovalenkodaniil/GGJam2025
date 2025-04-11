﻿using System.Collections.Generic;

namespace _Core.Scripts.Employees
{
    public class EmployeeData
    {
        public EmployeeConfig Config;
        public List<CharacterAttribute> Characteristics;

        public EmployeeData(EmployeeConfig config, List<CharacterAttribute> characterictics)
        {
            this.Config = config;
            this.Characteristics = characterictics;
        }
    }
}