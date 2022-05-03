using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Rodser.MergeTwins.UI
{
    public class GameOverUI : MonoBehaviour
    {
        [SerializeField] private Button resetButton = null;
        [SerializeField] private Button exitButton = null;

        private void Start()
        {
            this.resetButton.onClick.AddListener(Reset);
            this.exitButton.onClick.AddListener(Quit);
        }

        private void Reset()
        {
            Debug.Log("Load Scene");
            Game.OnClickButton(this);
            Game.StartGame();
            SceneManager.LoadSceneAsync(Game.Level);
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