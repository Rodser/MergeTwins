using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Rodser.MergeTwins.UI
{
    public class GameOverUI : MonoBehaviour
    {
        [SerializeField] private Button resetButton = null;
        [SerializeField] private Button exitButton = null;
        
        private void Start()
        {
            this.resetButton.onClick.AddListener(ReplayLevel);
            this.exitButton.onClick.AddListener(Quit);
        }

        private void ReplayLevel()
        {
            Debug.Log("Load Scene");
            Game.OnClickButton(this);
            //Game.GameManager.LevelUp(1);
            Game.GameManager.SceneUI.SetStartConfig();
            Game.StartGame();
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