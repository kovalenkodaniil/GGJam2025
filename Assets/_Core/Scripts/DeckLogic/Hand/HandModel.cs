using System;
using System.Collections.Generic;
using System.Linq;
using _Core.Scripts.DeckLogic.Trash;
using _Core.Scripts.Employees;
using _Core.Scripts.OfficeScripts;
using _Core.StaticProvider;
using JetBrains.Annotations;
using UnityEngine;

namespace _Core.Scripts.DeckLogic.Hand
{
    public class HandModel
    {
        private TrashModel m_trashModel;
        public int CardsPerTurn;
        public List<EmployeeData> Employees;
        
        private List<OfficeConfig> _officeConfigs;
        public event Action<EmployeeData> EmployeesAdded;
        public event Action<EmployeeData> EmployeesRemoved;

        public HandModel(TrashModel trashModel)
        {
            Employees = new List<EmployeeData>();

            _officeConfigs = new List<OfficeConfig>();
            _officeConfigs.AddRange(StaticDataProvider.Get<OfficeDataProvider>().asset.configs);
            
            m_trashModel = trashModel;
            CardsPerTurn = _officeConfigs.FirstOrDefault(officeConfig => officeConfig.level == 1)?.cardsPerTurn ?? 3;
            if (CardsPerTurn == 0)
            {
                CardsPerTurn = 3;
            }
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