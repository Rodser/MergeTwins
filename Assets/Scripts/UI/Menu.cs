using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MiniIT.Test.UI
{
    public class Menu : MonoBehaviour
    {
        [SerializeField] private GameObject sceneUI;
        [SerializeField] private Image panelMenu = null;
        [SerializeField] private Button settingButton = null;
        [SerializeField] private Button playButton = null;
        [SerializeField] private Button exitButton = null;

        private void Start()
        {
            this.settingButton.onClick.AddListener(OpenSetting);
            this.playButton.onClick.AddListener(LoadScene);
            this.exitButton.onClick.AddListener(Quit);
        }

        private void OpenSetting()
        {
            Debug.Log("Setting");
        }

        private void LoadScene()
        {
            Debug.Log("Load Scene");
            DontDestroyOnLoad(sceneUI);
            SceneManager.LoadSceneAsync(Game.Level);
            this.gameObject.SetActive(false);
        }

        private void Quit()
        {
            Debug.Log("Quit application!");
            Application.Quit();
        }
    }
}