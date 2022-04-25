#if UNITY_EDITOR
using UnityEditor;
#endif
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace MiniIT.Test.UI
{
    public class Menu : MonoBehaviour
    {
        
        [SerializeField] private Button settingButton = null;
        [SerializeField] private Button playButton = null;
        [SerializeField] private Button resetButton = null;
        [SerializeField] private Button exitButton = null;

        private void Start()
        {
            this.settingButton.onClick.AddListener(OpenSetting);
            this.playButton.onClick.AddListener(LoadScene);
            this.resetButton.onClick.AddListener(Reset);
            this.exitButton.onClick.AddListener(Quit);
        }

        private void OpenSetting()
        {
            Debug.Log("Setting");
            Game.OnClickButton(this);
        }

        private void LoadScene()
        {
            Debug.Log("Load Scene");
            Game.OnClickButton(this);
            Game.StartGame();
            SceneManager.LoadSceneAsync(Game.Level);
        }

        private void Reset()
        {
            Time.timeScale = 1;
            this.gameObject.SetActive(false);
            Game.OnClickButton(this);
        }

        private void Quit()
        {
            Debug.Log("Quit application!");
            Game.OnClickButton(this);
#if UNITY_EDITOR
            EditorApplication.ExitPlaymode();
#endif
            Application.Quit();
        }
    }
}