#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.UI;
using YG;

namespace Rodser.MergeTwins.UI
{
    public class GameOverUI : MonoBehaviour
    {
        [SerializeField] private Button resetButton = null;
        [SerializeField] private Button exitButton = null;
        [SerializeField] private Button play;

        private void Start()
        {
            this.resetButton.onClick.AddListener(ReplayLevel);
            this.exitButton.onClick.AddListener(Quit);
            this.play.onClick.AddListener(PlayOn);
        }

        private void OnEnable() => YandexGame.CloseVideoEvent += ContinuePlay;
        private void OnDisable() => YandexGame.CloseVideoEvent -= ContinuePlay;

        private void ReplayLevel()
        {
            Debug.Log("Load Scene");
            Game.OnClickButton(this);
            Game.GameManager.SceneUI.SetStartConfig();
            Game.StartGame();
        }

        private void PlayOn()
        {
            Debug.Log("RewardedShow");
            Game.OnClickButton(this);
            Game.GameManager.YG._RewardedShow(Game.GameManager.IdReward);
#if UNITY_EDITOR
            ContinuePlay(Game.GameManager.IdReward);
            Game.GameManager.ContinuePlay();
#endif
        }

        private void ContinuePlay(int id)
        {
            Debug.Log("RewardClose " + id);

            if (id == Game.GameManager.IdReward)
            {
                Debug.Log("RewardClose");
                Game.GameManager.SceneUI.SetStartConfig();
                Game.IsPlaying = true;
                Time.timeScale = 1;
                this.gameObject.SetActive(false);
            }
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