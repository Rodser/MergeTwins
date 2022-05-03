using Rodser.MergeTwins.Items;
using Rodser.MergeTwins.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Rodser.MergeTwins
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private LevelManager[] levels;
        [SerializeField] private Grid grid = null;
        [SerializeField] private UIManager sceneUI = null;

        private void Awake()
        {
            Game.GameManager = this;
        }

        public void StartLevel(int level)
        {
            LevelManager lvl = levels[level];
            this.grid.CreateGrid(lvl.Width, lvl.Height, lvl.Cell, lvl.GetItem(Game.Level)
                                , lvl.StartCountItems, lvl.TimeBetweenSpawn, lvl.TimeToDefeat);
        }

        public ItemAsset GetItem(int level)
        {
            return levels[level].GetItem(Game.Level);
        }
    }
}