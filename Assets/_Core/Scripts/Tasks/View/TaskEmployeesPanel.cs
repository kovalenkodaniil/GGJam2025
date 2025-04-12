using System;
using System.Collections.Generic;
using _Core.Scripts.Employees;
using _Core.Scripts.Employees.View;
using UnityEngine;

namespace _Core.Scripts.Tasks.View
{
    public class TaskEmployeesPanel : MonoBehaviour
    {
        [SerializeField] private RectTransform m_rect;
        [SerializeField] private List<SlotView> m_slots;

        public event Action<EmployeeData> EmpoyeesAdded;

        public event Action<EmployeeData> EmpoyeesRemoved;

        public event Action EmpoyeesTrashed;

        public event Action EmpoyeesReturn;

        public void Init()
        {
            Reset();
        }

        public bool IsPositionInPanel(Vector3 position)
        {
            Vector3 localPosition = m_rect.InverseTransformPoint(position);

            return m_rect.rect.Contains(localPosition);
        }

        public void AddEmployee(EmployeeData data, EmployeesWidget widget)
        {
            SlotView slot = m_slots.Find(sell => sell.IsEmpty);
            slot.SetWidget(widget, data);

            EmpoyeesAdded?.Invoke(data);
        }

        public void RemoveEmployee(EmployeeData data)
        {
            foreach (SlotView slot in m_slots)
            {
                if (slot.IsEmpty)
                    continue;

                if (slot.Data.Config.id == data.Config.id)
                {
                    slot.Reset();
                    break;
                }
            }

            EmpoyeesRemoved?.Invoke(data);
        }

        public void Reset()
        {
            m_slots.ForEach(sell => sell.Reset());
        }

        public void TrashEmployee()
        {
            EmpoyeesTrashed?.Invoke();
        }

        public void ReturnEmployees()
        {
            EmpoyeesReturn?.Invoke();
        }
    }
}