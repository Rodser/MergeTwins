﻿using MiniIT.Test.UI;
using UnityEngine;

namespace MiniIT.Test
{
    public class GameStarter : MonoBehaviour
    {
        [SerializeField] private LevelManager levelManager = null;
        [SerializeField] private Grid grid = null;
        [SerializeField] private UIManager sceneUI = null;

        private void Awake()
        {
            Game.SetLevelManager(this.levelManager);
            this.grid.CreateGrid();
            
            ReloadUI();
        }

        private void ReloadUI()
        {
            this.sceneUI.gameObject.SetActive(false);
            this.sceneUI.gameObject.SetActive(true);
        }
    }
}