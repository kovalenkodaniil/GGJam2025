using UnityEngine;

namespace _Core.Scripts.Employees.View
{
    public class SlotView : MonoBehaviour
    {
        public bool IsEmpty { get; private set; }

        public EmployeesWidget Widget { get; private set; }

        public EmployeeData Data { get; private set; }

        public void SetWidget(EmployeesWidget widget, EmployeeData employeeData)
        {
            Widget = widget;
            widget.transform.SetParent(this.transform);
            widget.transform.localPosition = Vector3.zero;

            Data = employeeData;

            IsEmpty = false;
        }

        public void Reset()
        {
            Widget = null;
            Data = null;

            IsEmpty = true;
        }
    }
}