using Rodser.MergeTwins.Items;
using Rodser.MergeTwins.UI;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using YG;

namespace Rodser.MergeTwins
{
    public class GameManager : MonoBehaviour, ISaveable
    {
        [SerializeField] private LevelManager[] levels;
        [SerializeField] private Grid grid = null;
        [SerializeField] private UIManager sceneUI = null;
        [SerializeField] private YandexGame yg = null;
        [Space(8f)]
        [SerializeField] private float timeToWin = 0.5f;

        public UIManager SceneUI => sceneUI;

        private LevelManager currentLevel = null;
        private int currentLevelIndex = 0;
        private int currentLevelItem = 0;
        private int indexScene = 1;
        public bool[] openLevels = null;

        private void Awake()
        {
            Game.GameManager = this;
            DontDestroyOnLoad(this);
            openLevels = new bool[levels.Length];
            openLevels[currentLevelIndex] = true;
        }

        private void OnEnable() => YandexGame.GetDataEvent += Load;
        private void OnDisable() => YandexGame.GetDataEvent -= Load;

        public void StartLevel()
        {
            SceneManager.LoadSceneAsync(indexScene);
            Time.timeScale = 1;
            currentLevel = levels[currentLevelIndex];
            this.grid.CreateGrid(currentLevel.Width, 
                                 currentLevel.Height, 
                                 currentLevel.Cell, 
                                 GetItem(currentLevelItem), 
                                 currentLevel.StartCountItems, 
                                 currentLevel.TimeBetweenSpawn, 
                                 currentLevel.TimeToDefeat);
            sceneUI.gameObject.SetActive(false);
            sceneUI.gameObject.SetActive(true);
            sceneUI.SetLevelText(currentLevelIndex);
        }

        public ItemAsset GetItem(int levelItem)
        {
            return currentLevel.GetItem(levelItem);
        }

        public int GetMultiplier()
        {
            return currentLevel.MoneyMultiplier;
        }

        public void ClearLevel()
        {
            this.currentLevelIndex = 0; 
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
            Game.IsPlaying = false;
            openLevels[++currentLevelIndex] = true;
            yield return new WaitForSeconds(timeToWin);

            this.yg._FullscreenShow();
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
            YandexGame.savesData.indexLevel = currentLevelIndex;
            YandexGame.savesData.openLevels = openLevels;
            YandexGame.SaveProgress();
            Debug.Log("Save!");
        }

        public void Load()
        {
            sceneUI.CoinUI.Load();
            currentLevelIndex = YandexGame.savesData.indexLevel;
            openLevels = YandexGame.savesData.openLevels;
            Debug.Log("Load!");
            sceneUI.SetLevelText(currentLevelIndex);
        }
    }
}