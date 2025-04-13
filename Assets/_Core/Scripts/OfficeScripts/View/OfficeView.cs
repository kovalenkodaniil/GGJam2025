﻿using System;
using System.Collections.Generic;
using _Core.Scripts.Tasks.View;
using UnityEngine;
using UnityEngine.UI;

namespace _Core.Scripts.OfficeScripts.View
{
    public class OfficeView : MonoBehaviour
    {
        [SerializeField] private Button m_taskButton;
        [SerializeField] private Button m_endTurnButton;
        [SerializeField] private HelpView m_helpView;
        [SerializeField] private List<TaskButton> m_taskButtons;
        [SerializeField] private List<OfficeCounterView> m_officeCounters;

        public List<TaskButton> TaskButtons => m_taskButtons;

        public List<OfficeCounterView> OfficeCounters => m_officeCounters;

        public event Action TackClicked;

        public event Action EndTurnClicked;

        public void Init()
        {
            m_taskButton.onClick.AddListener(OpenTask);
            m_endTurnButton.onClick.AddListener(EndTurn);
        }

        public void OnDisable()
        {
            m_taskButton.onClick.RemoveListener(OpenTask);
            m_endTurnButton.onClick.RemoveListener(EndTurn);
        }

        public void SetEnableTurnButton(bool isEnabling)
        {
            m_endTurnButton.interactable = isEnabling;
        }

        public void OpenHelp()
        {
            m_helpView.Open();
        }

        private void OpenTask()
        {
            TackClicked?.Invoke();
        }

        private void EndTurn()
        {
            EndTurnClicked?.Invoke();
        }
    }
}