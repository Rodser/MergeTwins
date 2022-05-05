using UnityEngine;
using UnityEngine.UI;

namespace Rodser.MergeTwins.UI
{
    public class NextUI : MonoBehaviour
    {
        [SerializeField] private Button nextLevel;
        [SerializeField] private Button replayLevel;

        private void Start()
        {
            this.nextLevel.onClick.AddListener(StartNextLevel);
            this.replayLevel.onClick.AddListener(ReplayLevel);
        }

        private void ReplayLevel()
        {
            Debug.Log("Load Scene Replay");
            Game.OnClickButton(this);
            Game.GameManager.SceneUI.SetStartConfig();
            Game.StartGame();
        }

        private void StartNextLevel()
        {
            Debug.Log("NextLevel");
            Debug.Log("Load Scene");
            Game.OnClickButton(this);
            Game.GameManager.SceneUI.SetStartConfig();
            Game.GameManager.RaisingCurrentLevel();
            Game.StartGame();
        }
    }
}