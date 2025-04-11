using System.Collections.Generic;
using _Core.Scripts.DeckLogic.Trash;
using _Core.Scripts.Employees;
using UnityEngine;

namespace _Core.Scripts.DeckLogic.Deck
{
    public class DeckModel
    {
        public List<EmployeeData> Employees;
        private readonly TrashModel _trash;
        public bool IsEmpty => Employees.Count == 0;

        public DeckModel(List<EmployeeData> employees, TrashModel trash)
        {
            Employees = new List<EmployeeData>();
            Employees.AddRange(employees);
            
            _trash = trash;
        }

        public void AddCard(EmployeeData employee)
        {
            Employees.Add(employee);
        }

        private void AddCardsFromTrash()
        {
            List<EmployeeData> trashList = _trash.GetAllCards();
            AddCardByList(trashList);
            Shuffle();
        }
        
        public void AddCardByList(List<EmployeeData> employees)
        {
            Employees.AddRange(employees);
        }
        
        public List<EmployeeData> GetCards(int count)
        {
            List<EmployeeData> recievedEmployees = new List<EmployeeData>();
            
            for (int i = 1; i < count; i++)
            {
                if (Employees.Count == 0)
                {
                    AddCardsFromTrash();
                }
                
                recievedEmployees.Add(this.GetCard());
            }
            return recievedEmployees;
        }

        private EmployeeData GetCard()
        {
            EmployeeData lastItem = Employees[^1];
            Employees.RemoveAt(Employees.Count - 1);

            return lastItem;
        }

        public void Shuffle()
        {
            int n = Employees.Count;  
            while (n > 1) 
            {  
                n--;  
                int k = Random.Range(0, n + 1);  
                (Employees[k], Employees[n]) = (Employees[n], Employees[k]); // Обмен значений
            }  
        }
    }
}