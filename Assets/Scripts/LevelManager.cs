using MiniIT.Test.Items;
using UnityEngine;

namespace MiniIT.Test
{
    [CreateAssetMenu(fileName = "LevelManager", menuName = "Game/LevelManager", order = 3)]
    public class LevelManager : ScriptableObject
    {
        [SerializeField] private Item[] items;

        public Item GetItem(int level)
        {
            return this.items[level];
        }
    }
}