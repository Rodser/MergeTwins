using Rodser.MergeTwins.Items;
using UnityEngine;

namespace Rodser.MergeTwins
{
    [CreateAssetMenu(fileName = "LevelManager", menuName = "Game/LevelManager", order = 3)]
    public class LevelManager : ScriptableObject
    {
        [SerializeField] private ItemAsset[] items = null;
        [SerializeField] private int moneyMultiplier = 1;
        [Space(10)]
        [Header("Configuration of Grid")]
        [SerializeField] private int width = 3;
        [SerializeField] private int height = 3;
        [SerializeField] private CellAsset cell = null;
        [Space(10)]
        [Header("Start configuration")]
        [SerializeField] private int startCountItems = 1;
        [SerializeField] private float timeBetweenSpawn = 1f;
        [SerializeField] private float timeToDefeat = 5f;

        public int Width => width;
        public int Height => height;
        public CellAsset Cell => cell;
        public int StartCountItems => startCountItems;
        public float TimeBetweenSpawn => timeBetweenSpawn;
        public float TimeToDefeat => timeToDefeat;
        public int MoneyMultiplier => moneyMultiplier;

        public ItemAsset GetItem(int levelItem)
        {
            if (levelItem >= this.items.Length)
            {
                return null;
            }
            else if (levelItem == this.items.Length - 1)
            {
                Game.GameManager.RaisingTheLevel();
                return this.items[levelItem];
            }
            else
            {
                return this.items[levelItem];
            }
        }
    }
}