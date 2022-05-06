using Rodser.MergeTwins.Items;
using Rodser.MergeTwins.UI;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Rodser.MergeTwins
{
    public class GameManager : MonoBehaviour, ISaveable
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
            Load();
        }

        public void StartLevel()
        {
            SceneManager.LoadSceneAsync(indexScene);
            Time.timeScale = 1;
            this.grid.CreateGrid(currentLevel.Width, currentLevel.Height, currentLevel.Cell, GetItem(currentLevelItem)
                                , currentLevel.StartCountItems, currentLevel.TimeBetweenSpawn, currentLevel.TimeToDefeat);

        }

        public ItemAsset GetItem(int levelItem)
        {
            return currentLevel.GetItem(levelItem);
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
                Debug.Log($"currentLevelIndex : {currentLevelIndex}");
                StartCoroutine(RaisingTheLevelRoutine(true));
            }
        }

        internal void RaisingCurrentLevel()
        {
            currentLevel = levels[++currentLevelIndex];
            Save();
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

        public void Save()
        {
            sceneUI.CoinUI.Save();
            PlayerPrefs.SetInt("level", currentLevelItem);
            PlayerPrefs.Save();
            Debug.Log("Save!");
        }

        public void Load()
        {
            sceneUI.CoinUI.Load();
            currentLevelIndex = PlayerPrefs.GetInt("level");
            currentLevel = levels[currentLevelIndex];
            Debug.Log("Load!");
        }
    }
}