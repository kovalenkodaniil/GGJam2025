﻿using System.Collections.Generic;
using _Core.Scripts.Employees;
using JetBrains.Annotations;

namespace _Core.Scripts.DeckLogic.Hand
{
    public class HandModel
    {
        public List<EmployeeData> Employees;

        public HandModel()
        {
            Employees = new List<EmployeeData>();
        }

        public void AddCard(EmployeeData employee)
        {
            Employees.Add(employee);
        }

        [CanBeNull]
        public EmployeeData GetCard(EmployeeData searchableEmployee)
        {
            foreach (EmployeeData employee in Employees)
            {
                if (searchableEmployee.Config.id == employee.Config.id)
                {
                    return employee;
                }
            }

            return null;
        }
    }
}