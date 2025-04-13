using System;
using System.Collections.Generic;
using _Core.Scripts.Employees;
using _Core.StaticProvider;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Core.Scripts.Tasks.View
{
    public class TaskView : MonoBehaviour
    {
        [SerializeField] private GameObject m_conatainer;
        [SerializeField] private TMP_Text m_name;
        [SerializeField] private TMP_Text m_description;
        [SerializeField] private TMP_Text m_comment;
        [SerializeField] private TMP_Text m_expCounter;
        [SerializeField] private CanvasGroup m_canvasGroup;
        [SerializeField] private TaskEmployeesPanel m_empoyeesPanel;
        [SerializeField] private Button m_completeButton;
        [SerializeField] private Button m_closeButton;
        [SerializeField] private List<TaskConditionView> m_conditionView;
        [SerializeField] private List<TaskConditionView> m_currentSkillView;
        [SerializeField] private List<TaskRewardView> m_rewardView;

        private Tween _tween;
        private Sequence _tweenSequence;

        public event Action OnComplete;

        public TaskEmployeesPanel EmployeesPanel => m_empoyeesPanel;

        public string Name
        {
            set => m_name.text = value;
        }
        public string Description
        {
            set => m_description.text = value;
        }

        public string Comment
        {
            set => m_comment.text = value;
        }

        public void SetExp(int value)
        {
            m_expCounter.text = $"+{value}";
        }

        public void Open()
        {
            m_conatainer.SetActive(true);

            m_empoyeesPanel.Init();

            m_completeButton.onClick.AddListener(CompleteTask);
            m_closeButton.onClick.AddListener(CloseFromButton);

            PlayOpenAnim();
        }

        public void Close()
        {
            m_completeButton.onClick.RemoveListener(CompleteTask);
            m_closeButton.onClick.RemoveListener(CloseFromButton);

            m_empoyeesPanel.ReturnEmployees();

            PlayCloseAnim();
        }

        public void SetEnablingCompleteButton(bool isEnabling)
        {
            m_completeButton.interactable = isEnabling;
        }

        public void SetConditionCounters(List<CharacterAttribute> attributes)
        {
            m_conditionView.ForEach(view => view.Disable());

            attributes.ForEach(attribute =>
            {
                TaskConditionView view = m_conditionView.Find(view => view.Type == attribute.type);

                view.SetCount(attribute.value);
            });
        }

        public void SetCurrentCounters(List<CharacterAttribute> attributes)
        {
            m_currentSkillView.ForEach(view => view.Disable());

            attributes.ForEach(attribute =>
            {
                TaskConditionView view = m_currentSkillView.Find(view => view.Type == attribute.type);

                view.SetCount(attribute.value);
            });
        }

        public void SetRewardCounters(List<RewardAttribute> rewards)
        {
            m_rewardView.ForEach(view => view.Disable());

            rewards.ForEach(attribute =>
            {
                m_rewardView.Find(view => view.Type == attribute.type).SetCount(attribute.value);
            });
        }

        private void CloseFromButton()
        {
            SoundManager.Instance.PlaySfx(StaticDataProvider.Get<SoundDataProvider>().asset.buttonClick);

            Close();
        }

        private void CompleteTask()
        {
            SoundManager.Instance.PlaySfx(StaticDataProvider.Get<SoundDataProvider>().asset.stampClick);

            m_empoyeesPanel.TrashEmployee();

            OnComplete?.Invoke();
        }

        private void PlayOpenAnim()
        {
            _tweenSequence = DOTween.Sequence();

            transform.localScale = new Vector3(0.2f,0.2f,0.2f);
            m_canvasGroup.alpha = 0;

            _tweenSequence.Append(transform.DOScale(1f, 0.2f));
            _tweenSequence.Join(m_canvasGroup.DOFade(1f, 0.2f));
        }

        private void PlayCloseAnim()
        {
            _tweenSequence = DOTween.Sequence();

            _tweenSequence.Append(transform.DOScale(0.2f, 0.2f));
            _tweenSequence.Join(m_canvasGroup.DOFade(0f, 0.2f));

            _tweenSequence.OnComplete(() => { m_conatainer.SetActive(false); });
        }
    }
}