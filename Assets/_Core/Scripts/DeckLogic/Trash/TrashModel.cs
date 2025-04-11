using System.Collections.Generic;
using _Core.Scripts.Employees;

namespace _Core.Scripts.DeckLogic.Trash
{
    public class TrashModel
    {
        public List<EmployeeData> Employees = new();

        public void AddCard(EmployeeData employee)
        {
            Employees.Add(employee);
        }

        public List<EmployeeData> GetAllCards()
        {
            List<EmployeeData> recievedEmployees = new List<EmployeeData>();
            recievedEmployees.AddRange(Employees);
            
            Employees.Clear();
            
            return recievedEmployees;
        }
    }
}