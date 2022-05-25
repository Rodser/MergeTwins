using System;
using UnityEditor;
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
            Game.GameManager.YG._RewardedShow(Game.GameManager.idReward);
            YandexGame.CloseVideoEvent += PlayInfinityLevel;
        }

        private void PlayInfinityLevel(int id)
        {
            if (id == Game.GameManager.idReward)
            {
                Debug.Log("RewardClose");
                Time.timeScale = 1;
                Game.GameManager.SceneUI.SetStartConfig();
                this.gameObject.SetActive(false);
                Game.IsPlaying = true;
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