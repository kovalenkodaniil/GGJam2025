using System;
using System.Collections.Generic;
using System.Linq;
using _Core.Scripts.Employees;
using R3;
using UnityEngine;

namespace _Core.Scripts.Tasks
{
    public class TaskModel
    {
        public TaskConfig Task;
        public List<EmployeeData> Employees = new();
        public ReactiveProperty<bool> IsReady = new(false);
        public List<CharacterAttribute> CurrentConditions = new();
        private TaskRewardConfig _taskReward;

        public void Init(TaskConfig task)
        {
            Task = task;
            _taskReward = task.rewards;
        }

        public void AddEmployee(EmployeeData employee)
        {
            Employees.Add(employee);
            CalculateCurrentConditions();
            CheckTaskConditions();
        }

        public EmployeeData RemoveEmployee(EmployeeData searchableEmployee)
        {
            foreach (EmployeeData employee in Employees)
            {
                if (searchableEmployee.Config.id == employee.Config.id)
                {
                    Employees.Remove(searchableEmployee);

                    CalculateCurrentConditions();
                    CheckTaskConditions();

                    return searchableEmployee;
                }
            }

            return null;
        }

        public void CheckTaskConditions()
        {
            foreach (CharacterAttribute attribute in Task.conditions)
            {
                CharacterAttribute currentAttribute = CurrentConditions.FirstOrDefault(a => a.type == attribute.type);

                if (currentAttribute == null || attribute.value > currentAttribute.value)
                {
                    IsReady.Value = false;
                    return;
                }
            }
            IsReady.Value = true;
        }

        private void CalculateCurrentConditions()
        {
            CurrentConditions.Clear();

            foreach (EmployeeData employee in Employees)
            {
                foreach (CharacterAttribute attribute in employee.Characteristics)
                {
                    CharacterAttribute currentAttribute = CurrentConditions.FirstOrDefault(a => a.type == attribute.type);
                    int currentValue = currentAttribute?.value ?? -1;

                    if (currentValue == -1)
                    {
                        CharacterAttribute newAttribute = new();
                        newAttribute.type = attribute.type;
                        newAttribute.value = attribute.value;

                        CurrentConditions.Add(newAttribute);
                    }
                    else
                    {
                        currentAttribute.value += attribute.value;
                    }
                }
            }

        }

        public TaskRewardConfig GetReward()
        {
            return _taskReward;
        }
    }
}