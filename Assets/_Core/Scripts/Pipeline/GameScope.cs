using _Core.Scripts.MainMenuScripts;
using UnityEngine;
using VContainer;
using VContainer.Unity;

namespace _Core.Scripts.Pipeline
{
    public class GameScope: LifetimeScope
    {
        [SerializeField] private MainMenu m_menu;

        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(m_menu);
        }
    }
}