using System;
using System.Collections.Generic;
using System.Linq;
using _Core.StaticProvider;
using R3;
using UnityEngine;

namespace _Core.Scripts.TurnManagerScripts
{
    public class TurnManager
    {
        public Subject<Unit> OnTurnStarted;
        public Subject<Unit> OnPlayerActionStarted;
        public Subject<Unit> OnTurnEnded;
        
        public int TurnNumber;
        public TurnConfig CurrentTurn;
        private List<TurnConfig> m_turnConfigs;

        public EnumTurnState CurrentState { get; private set; }

        private int _stateAmount;

        public TurnManager()
        {
            _stateAmount = Enum.GetNames(typeof(EnumTurnState)).Length;

            OnTurnStarted = new Subject<Unit>();
            OnPlayerActionStarted = new Subject<Unit>();
            OnTurnEnded = new Subject<Unit>();
            
            m_turnConfigs = new List<TurnConfig>();
            m_turnConfigs.AddRange(StaticDataProvider.Get<TurnDataProvider>().asset.turns);
        }

        public void Dispose()
        {
            OnTurnStarted.OnCompleted();
            OnPlayerActionStarted.OnCompleted();
            OnTurnEnded.OnCompleted();
        }

        public void Init()
        {
            CurrentState = EnumTurnState.StartTurn;
            StartStep(EnumTurnState.StartTurn);
        }

        public void NextStep()
        {
            CurrentState = (EnumTurnState) (((int) CurrentState + 1) % _stateAmount);
            StartStep(CurrentState);
        }

        private void StartStep(EnumTurnState state)
        {
            switch (state)
            {
                case EnumTurnState.StartTurn:
                    ChangeTurn();
                    OnTurnStarted.OnNext(Unit.Default);
                    break;

                case EnumTurnState.PlayerAction:
                    OnPlayerActionStarted.OnNext(Unit.Default);
                    break;

                case EnumTurnState.EndTurn:
                    OnTurnEnded.OnNext(Unit.Default);
                    break;
            }
        }

        private void ChangeTurn()
        {
            TurnNumber++;
            CurrentTurn = m_turnConfigs.FirstOrDefault(a => a.turnNumber == TurnNumber);
        }
    }
}