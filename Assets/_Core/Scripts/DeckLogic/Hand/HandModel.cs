using System;
using System.Collections.Generic;
using _Core.Scripts.DeckLogic.Trash;
using _Core.Scripts.Employees;
using JetBrains.Annotations;
using UnityEngine;

namespace _Core.Scripts.DeckLogic.Hand
{
    public class HandModel
    {
        private TrashModel m_trashModel;

        public List<EmployeeData> Employees;

        public event Action<EmployeeData> EmployeesAdded;
        public event Action<EmployeeData> EmployeesRemoved;

        public HandModel(TrashModel trashModel)
        {
            Employees = new List<EmployeeData>();

            m_trashModel = trashModel;
        }

        public void DiscardHand()
        {
            Employees.ForEach(card =>
            {
                m_trashModel.AddCard(card);

                EmployeesRemoved?.Invoke(card);
            });

            Employees.Clear();
        }

        public void AddCard(EmployeeData employee)
        {
            Employees.Add(employee);

            EmployeesAdded?.Invoke(employee);
        }

        [CanBeNull]
        public EmployeeData GetCard(EmployeeData searchableEmployee)
        {
            foreach (EmployeeData employee in Employees)
            {
                if (searchableEmployee.Config.id == employee.Config.id)
                {
                    Employees.Remove(searchableEmployee);

                    EmployeesRemoved?.Invoke(employee);
                    return employee;
                }
            }

            return null;
        }
    }
}