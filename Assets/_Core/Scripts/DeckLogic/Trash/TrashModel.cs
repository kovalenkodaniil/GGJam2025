using System.Collections.Generic;
using _Core.Scripts.Employees;
using R3;

namespace _Core.Scripts.DeckLogic.Trash
{
    public class TrashModel
    {
        public List<EmployeeData> Employees;

        public ReactiveProperty<int> CardCount;

        public TrashModel()
        {
            Employees = new();

            CardCount = new ReactiveProperty<int>();
            CardCount.Value = 0;
        }

        public void AddCard(EmployeeData employee)
        {
            Employees.Add(employee);

            CardCount.Value = Employees.Count;
        }

        public List<EmployeeData> GetAllCards()
        {
            List<EmployeeData> recievedEmployees = new List<EmployeeData>();
            recievedEmployees.AddRange(Employees);

            Employees.Clear();

            CardCount.Value = Employees.Count;

            return recievedEmployees;
        }
    }
}