using System.Collections.Generic;
using _Core.Scripts.DeckLogic.Trash;
using _Core.Scripts.Employees;
using R3;
using UnityEngine;

namespace _Core.Scripts.DeckLogic.Deck
{
    public class DeckModel
    {
        public List<EmployeeData> Employees;
        private readonly TrashModel _trash;
        public bool IsEmpty => Employees.Count == 0;

        public ReactiveProperty<int> CardCount;

        public DeckModel(TrashModel trash)
        {
            Employees = new List<EmployeeData>();

            CardCount = new ReactiveProperty<int>(0);

            _trash = trash;
        }

        public void Init(List<EmployeeData> employees)
        {
            Employees.AddRange(employees);
            Shuffle();
        }

        public void AddCard(EmployeeData employee)
        {
            Employees.Add(employee);

            CardCount.Value = Employees.Count;
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

            for (int i = 0; i < count; i++)
            {
                if (Employees.Count == 0)
                {
                    AddCardsFromTrash();
                }

                recievedEmployees.Add(this.GetCard());

                CardCount.Value = Employees.Count;
            }

            return recievedEmployees;
        }

        private EmployeeData GetCard()
        {
            EmployeeData lastItem = Employees[^1];
            Employees.RemoveAt(Employees.Count - 1);

            return lastItem;
        }

        private void Shuffle()
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