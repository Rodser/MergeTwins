using UnityEngine;

namespace MiniIT.Test
{
    public class GameStarter : MonoBehaviour
    {
        [SerializeField] private LevelManager levelManager = null;
        [SerializeField] private Grid grid = null;


        private void Awake()
        {
            Game.SetLevelManager(this.levelManager);
            this.grid.CreateGrid();
        }
    }
}