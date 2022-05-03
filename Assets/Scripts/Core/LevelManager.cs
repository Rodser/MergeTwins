using Rodser.MergeTwins.Items;
using UnityEngine;

namespace Rodser.MergeTwins
{
    [CreateAssetMenu(fileName = "LevelManager", menuName = "Game/LevelManager", order = 3)]
    public class LevelManager : ScriptableObject
    {
        [SerializeField] private ItemAsset[] items = null;

        [Header("Configuration of Grid")]
        [SerializeField] private int width = 3;
        [SerializeField] private int height = 3;
        // [SerializeField] private float spaceBetweenCells = 2f;
        [SerializeField] private Cell cell = null;

        [Header("Start configuration")]
        [SerializeField] private int startCountItems = 1;
        [SerializeField] private float timeBetweenSpawn = 1f;
        [SerializeField] private float timeToDefeat = 5f;

        public int Width => width;
        public int Height => height;
        public Cell Cell => cell;
        public int StartCountItems => startCountItems;
        public float TimeBetweenSpawn => timeBetweenSpawn;
        public float TimeToDefeat => timeToDefeat;

        public ItemAsset GetItem(int level)
        {
            return level >= this.items.Length ? null : this.items[level];
        }
    }
}