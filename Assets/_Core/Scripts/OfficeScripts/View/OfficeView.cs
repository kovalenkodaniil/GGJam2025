using System;
using System.Collections.Generic;
using _Core.Scripts.MainMenuScripts;
using _Core.Scripts.Tasks.View;
using _Core.Scripts.TurnManagerScripts;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using VContainer;

namespace _Core.Scripts.OfficeScripts.View
{
    public class OfficeView : MonoBehaviour
    {
        [Inject] private MainMenu m_menu;
        [Inject] private TurnManager m_turnManager;

        [SerializeField] private Button m_taskButton;
        [SerializeField] private Button m_endTurnButton;
        [SerializeField] private HelpView m_helpView;
        [SerializeField] private SettingView m_settings;
        [SerializeField] private List<TaskButton> m_taskButtons;
        [SerializeField] private List<OfficeCounterView> m_officeCounters;
        [SerializeField] private ExpCounterView m_expCounter;
        [SerializeField] private TMP_Text m_turnCounter;

        private CompositeDisposable m_disposable;

        public List<TaskButton> TaskButtons => m_taskButtons;

        public List<OfficeCounterView> OfficeCounters => m_officeCounters;

        public ExpCounterView ExpCounter => m_expCounter;

        public event Action TackClicked;

        public event Action EndTurnClicked;

        public void Init()
        {
            m_taskButton.onClick.AddListener(OpenTask);
            m_endTurnButton.onClick.AddListener(EndTurn);

            m_disposable = new CompositeDisposable();

            m_turnManager.TurnNumber
                .Subscribe(UpdateTurnNumber)
                .AddTo(m_disposable);
        }

        public void OnDisable()
        {
            m_disposable?.Dispose();

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

        public void OpenSetting()
        {
            m_settings.Open();
        }

        public void BackToMenu()
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName("MainMenu"));
            SceneManager.UnloadSceneAsync("Office");

            m_menu.Open();
        }

        private void OpenTask()
        {
            TackClicked?.Invoke();
        }

        private void EndTurn()
        {
            EndTurnClicked?.Invoke();
        }

        private void UpdateTurnNumber(int turnNumber)
        {
            m_turnCounter.text = $"{turnNumber}";
        }
    }
}