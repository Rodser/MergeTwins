using Rodser.MergeTwins.Items;
using Rodser.MergeTwins.UI;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Rodser.MergeTwins
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private LevelManager[] levels;
        [SerializeField] private Grid grid = null;
        [SerializeField] private UIManager sceneUI = null;
        [SerializeField] private float timeToWin = 0.5f;

        public UIManager SceneUI => sceneUI;

        private LevelManager currentLevel = null;
        private int currentLevelIndex = 0;
        private int currentLevelItem = 0;
        private int indexScene = 1;

        private void Awake()
        {
            Game.GameManager = this;
            currentLevel = levels[currentLevelIndex];
            DontDestroyOnLoad(this);
        }

        public void StartLevel()
        {
            SceneManager.LoadSceneAsync(indexScene);
            Time.timeScale = 1;
            this.grid.CreateGrid(currentLevel.Width, currentLevel.Height, currentLevel.Cell, GetItem(currentLevelItem)
                                , currentLevel.StartCountItems, currentLevel.TimeBetweenSpawn, currentLevel.TimeToDefeat);

        }

        internal void RaisingTheLevel()
        {
            if (currentLevelIndex == levels.Length - 1)
            {
                StartCoroutine(RaisingTheLevelRoutine(false));
                Debug.Log("last Level");
            }
            else
            {
                currentLevel = levels[++currentLevelIndex];
                Debug.Log($"currentLevelIndex : {currentLevelIndex}");
                StartCoroutine(RaisingTheLevelRoutine(true));
            }
        }

        private IEnumerator RaisingTheLevelRoutine(bool levelUp)
        {
            yield return new WaitForSeconds(timeToWin);

            Game.IsPlaying = false;
            if (levelUp)
            {
                Debug.Log("Win!!");
                Game.RiasingLevel(this);
            }
            else
            {
                Debug.Log("Victory!");
                Game.Victory(this);
            }
        }

        public ItemAsset GetItem(int levelItem)
        {
            return currentLevel.GetItem(levelItem);
        }
    }
}