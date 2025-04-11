using UnityEngine;
using UnityEngine.SceneManagement;

namespace _Core.Scripts.MainMenuScripts
{
    public class MainMenu : MonoBehaviour
    {
        public void Awake()
        {
            Screen.SetResolution(1920,1080, true);
        }

        public void LoadGameScene()
        {
            SceneManager.LoadSceneAsync("Office", LoadSceneMode.Additive);

            gameObject.SetActive(false);
        }

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}