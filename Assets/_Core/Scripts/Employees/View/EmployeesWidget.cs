using System.Collections.Generic;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace _Core.Scripts.Employees.View
{
    public class EmployeesWidget : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        public Subject<Unit> OnDragEnded = new ();
        public Subject<Unit> OnDragHasBegun = new();

        [SerializeField] private Image _portrait;
        [SerializeField] private TMP_Text m_name;
        [SerializeField] private GameObject _emptyState;
        [SerializeField] private GameObject _employeesState;
        [SerializeField] private List<TMP_Text> m_characteristicCounters;

        public Sprite Portrait { set => _portrait.sprite = value; }

        public string Name { set => m_name.text = value; }

        public List<TMP_Text> Counters => m_characteristicCounters;

        public bool IsEmpty { get; private set; }

        public void SetEmptyState()
        {
            IsEmpty = true;

            _employeesState.SetActive(false);
            _emptyState.SetActive(true);
        }

        public void SetCharacterState()
        {
            IsEmpty = false;

            _employeesState.SetActive(true);
            _emptyState.SetActive(false);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            OnDragHasBegun.OnNext(Unit.Default);
        }

        public void OnDrag(PointerEventData eventData)
        {
            transform.localPosition += new Vector3(eventData.delta.x, eventData.delta.y, 0);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            OnDragEnded.OnNext(Unit.Default);
        }
    }
}