using System;
using System.Collections.Generic;
using _Core.Scripts.Tasks.View;
using _Core.StaticProvider;
using DG.Tweening;
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
        [SerializeField] private List<TaskConditionView> m_conditionView;

        private Tween _tween;
        private Sequence _tweenSequence;

        public event Action OnDiscarded;

        public Sprite Portrait { set => _portrait.sprite = value; }

        public string Name { set => m_name.text = value; }

        public bool IsEmpty { get; private set; }

        public bool IsDragable { get; private set; }

        public void SetConditionCounters(List<CharacterAttribute> attributes)
        {
            m_conditionView.ForEach(view => view.Disable());

            attributes.ForEach(attribute =>
            {
                TaskConditionView view = m_conditionView.Find(view => view.Type == attribute.type);

                view.SetCount(attribute.value);
            });
        }

        public void PlayDrawAnimation(Transform startPosition, Vector3 slotPosition, Transform newParent, float drawDelay)
        {
            IsDragable = false;
            _tweenSequence = DOTween.Sequence();

            transform.SetParent(startPosition);
            transform.position = startPosition.position;
            transform.localScale = new Vector3(0.2f,0.2f,0.2f);
            transform.rotation = Quaternion.Euler(new Vector3(0,0,21));

            _tweenSequence.Append(transform.DOMove(slotPosition, 0.8f)).SetDelay(drawDelay);
            _tweenSequence.Join(transform.DOScale(1, 0.8f));
            _tweenSequence.Join(transform.DORotate(Vector3.zero, 0.8f));

            _tweenSequence.OnComplete(() =>
            {
                transform.SetParent(newParent);
                IsDragable = true;
            });
        }

        public void PlayDiscardAnimation(Vector3 discardPosition)
        {
            IsDragable = false;
            _tweenSequence = DOTween.Sequence();

            Vector3 midPoint = (transform.position + discardPosition) / 2;
            midPoint.y += 2;

            Vector3[] path = new Vector3[]
            {
                midPoint,
                discardPosition
            };

            _tweenSequence.Append(transform.DOPath(path, 1.6f, PathType.CatmullRom)).SetEase(Ease.Linear);
            _tweenSequence.Join(transform.DOScale(0.6f, 1.4f));
            _tweenSequence.Join(transform.DORotate(new Vector3(0,0,-21), 0.6f));

            _tweenSequence.OnComplete(() => OnDiscarded?.Invoke());
        }

        public void SetCharacterState()
        {
            IsEmpty = false;
            IsDragable = true;

            _employeesState.SetActive(true);
            _emptyState.SetActive(false);
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (IsDragable)
            {
                SoundManager.Instance.PlaySfx(StaticDataProvider.Get<SoundDataProvider>().asset.cardStartDraging);

                OnDragHasBegun.OnNext(Unit.Default);
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (IsDragable)
                transform.localPosition += new Vector3(eventData.delta.x, eventData.delta.y, 0);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (IsDragable)
                OnDragEnded.OnNext(Unit.Default);
        }
    }
}