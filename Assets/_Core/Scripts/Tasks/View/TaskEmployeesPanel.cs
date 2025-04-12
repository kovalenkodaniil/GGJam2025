using System;
using System.Collections.Generic;
using _Core.Scripts.Employees;
using _Core.Scripts.Employees.View;
using ObservableCollections;
using UnityEngine;

namespace _Core.Scripts.Tasks.View
{
    public class TaskEmployeesPanel : MonoBehaviour
    {
        private readonly ObservableList<EmployeeConfig> m_employees = new ();

        public IObservableCollection<EmployeeConfig> Employees => m_employees;

        [SerializeField] private RectTransform m_rect;
        [SerializeField] private List<EmployeesWidget> m_slots;

        public event Action<EmployeeData> EmpoyeesAdded;

        public event Action<EmployeeData> EmpoyeesRemoved;

        public void Init()
        {
            Reset();
        }

        public bool IsPositionInPanel(Vector3 position)
        {
            Vector3 localPosition = m_rect.InverseTransformPoint(position);

            return m_rect.rect.Contains(localPosition);
        }

        public void AddEmployee(EmployeeData data)
        {
            m_employees.Add(data.Config);

            EmployeesWidget characterView = m_slots.Find(sell => sell.IsEmpty);

            UpdateView(characterView, data.Config);

            EmpoyeesAdded?.Invoke(data);
        }

        public void RemoveEmployee(EmployeeData data)
        {
            m_employees.Remove(data.Config);

            EmpoyeesRemoved?.Invoke(data);
        }

        public void Reset()
        {
            m_slots.ForEach(sell => sell.SetEmptyState());
            m_employees.Clear();
        }

        private void UpdateView(EmployeesWidget view, EmployeeConfig data)
        {
            view.SetCharacterState();
            view.Portrait = data.icon;
            view.Name = data.name;

            for (int i = 0; i < data.characterictics.Count; i++)
            {
                view.Counters[i].text = data.characterictics[i].value.ToString();
            }
        }
    }
}