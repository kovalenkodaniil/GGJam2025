using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Core.Scripts.Employees.View
{
    public class EmployeeSlotView : MonoBehaviour
    {
        [SerializeField] private Image m_lockIcon;
        [SerializeField] public bool IsLock;

        public bool IsEmpty { get; set; }

        private Tween _tween;
        private Sequence _tweenSequence;

        public void PlayUnlockAnimation()
        {
            _tweenSequence = DOTween.Sequence();

            _tweenSequence.Append(m_lockIcon.transform.DOShakePosition(0.2f, 0.3f));
            _tweenSequence.Append(m_lockIcon.DOFade(0, 0.2f));
            _tweenSequence.Join(m_lockIcon.transform.DOScale(0, 0.2f));

            _tweenSequence.OnComplete(() =>
            {
                IsLock = false;
                m_lockIcon.gameObject.SetActive(false);
            });
        }
    }
}