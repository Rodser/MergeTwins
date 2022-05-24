using Rodser.MergeTwins.Items;
using Rodser.MergeTwins.UI;
using System.Collections;
using UnityEngine;
using YG;

namespace Rodser.MergeTwins
{
    public class GameManager : MonoBehaviour, ISaveable
    {
        [SerializeField] private LevelManager[] levels;
        [SerializeField] private GridAsset gridAsset = null;
        [SerializeField] private UIManager sceneUI = null;
        [SerializeField] private YandexGame yg = null;
        [Space(8f)]
        [SerializeField] private float timeToWin = 0.5f;


        private Grid grid = null;
        private LevelManager currentLevel = null;
        private int currentLevelIndex = 0;
        private int currentLevelItem = 0;
        
        public UIManager SceneUI => sceneUI;
        public bool[] openLevels = null;

        private void Awake()
        {
            Game.GameManager = this;
            this.openLevels = new bool[this.levels.Length];
            this.openLevels[this.currentLevelIndex] = true;
        }

        private void OnEnable() => YandexGame.GetDataEvent += Load;
        private void OnDisable() => YandexGame.GetDataEvent -= Load;

        public void StartLevel()
        {
            RemoveOldGrid();
            CreateGrid();
            Time.timeScale = 1;
            this.currentLevel = levels[this.currentLevelIndex];
            this.grid.BuildGrid(this.currentLevel.Width,
                                 this.currentLevel.Height,
                                 this.currentLevel.Cell,
                                 GetItem(this.currentLevelItem),
                                 this.currentLevel.StartCountItems,
                                 this.currentLevel.TimeBetweenSpawn,
                                 this.currentLevel.TimeToDefeat);
            this.sceneUI.gameObject.SetActive(false);
            this.sceneUI.gameObject.SetActive(true);
            this.sceneUI.SetLevelText(this.currentLevelIndex);
        }

        private void RemoveOldGrid()
        {
            if (this.grid != null)
            {
                this.grid.Remove();
            }
        }

        private void CreateGrid()
        {
            if (this.grid is null)
            {
                this.grid = new GameObject("Grid").AddComponent<Grid>();
                this.grid.SetSpace(this.gridAsset.SpaceBetweenCells);
            }
        }

        public ItemAsset GetItem(int levelItem)
        {
            return this.currentLevel.GetItem(levelItem);
        }

        public int GetMultiplier()
        {
            return this.currentLevel.MoneyMultiplier;
        }

        public void ClearLevel()
        {
            this.currentLevelIndex = 0; 
        }

        internal void RaisingTheLevel()
        {
            if (this.currentLevelIndex == this.levels.Length - 1)
            {
                StartCoroutine(RaisingTheLevelRoutine(false));
                Debug.Log("last Level");
            }
            else
            {
                Debug.Log($"currentLevelIndex : {this.currentLevelIndex}");
                StartCoroutine(RaisingTheLevelRoutine(true));
            }
        }

        internal void RaisingCurrentLevel()
        {
            this.currentLevel = this.levels[++this.currentLevelIndex];
            Save();
        }

        private IEnumerator RaisingTheLevelRoutine(bool levelUp)
        {
            Game.IsPlaying = false;
            this.openLevels[this.currentLevelIndex + 1] = true;
            yield return new WaitForSeconds(this.timeToWin);

            if (yg != null)
            {
                this.yg._FullscreenShow();
            }

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
            this.sceneUI.CoinUI.Save();
            YandexGame.savesData.indexLevel = this.currentLevelIndex;
            YandexGame.savesData.openLevels = this.openLevels;
            YandexGame.SaveProgress();
            Debug.Log("Save!");
        }

        public void Load()
        {
            this.sceneUI.CoinUI.Load();
            this.currentLevelIndex = YandexGame.savesData.indexLevel;
            this.openLevels = YandexGame.savesData.openLevels;
            Debug.Log("Load!");
            this.sceneUI.SetLevelText(this.currentLevelIndex);
        }
    }
}