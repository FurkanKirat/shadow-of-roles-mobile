namespace Managers
{
    using System.Collections;
    using UnityEngine;
    using UnityEngine.SceneManagement;
    using UnityEngine.UI;

    public class SceneManager : MonoBehaviour
    {
        private static int imageCount = 6;
        private static int currentImage;
        private static SceneManager instance;

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }

            currentImage = Random.Range(0, imageCount);
        }

        public static void SwitchScene(string sceneName)
        {
            instance.StartCoroutine(LoadSceneWithTransition(sceneName));
        }

        private static IEnumerator LoadSceneWithTransition(string sceneName)
        {
            UIManager.Instance.ShowLoadingScreen(true);

            yield return new WaitForSeconds(0.5f);

            SceneManager.LoadScene(sceneName);

            yield return new WaitForSeconds(0.5f);

            UIManager.Instance.ShowLoadingScreen(false);
        }

        public static void MainMenuScene()
        {
            SwitchScene("MainMenu");
        }

        public static void SettingsScene()
        {
            SwitchScene("Settings");
        }

        public static void ChangeLangScene()
        {
            SwitchScene("ChangeLanguage");
        }

        public static void OnClose()
        {
            UIManager.Instance.ShowConfirmationDialog("Are you sure you want to exit?", () =>
            {
                Application.Quit();
            });
        }

        public static void ToggleFullScreen()
        {
            Screen.fullScreen = !Screen.fullScreen;
        }
    }

}