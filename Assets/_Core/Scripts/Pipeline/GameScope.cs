using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Core.Scripts.Pipeline
{
    public class GameScope: LifetimeScope
    {
        [SerializeField]
        private GameScope _gameScope;

        protected override void Configure(IContainerBuilder builder)
        {

        }
    }
}