using MiniIT.Test.Items;
using UnityEngine;

namespace MiniIT.Test
{
    [CreateAssetMenu(fileName = "LevelManager", menuName = "Game/LevelManager", order = 3)]
    public class LevelManager : ScriptableObject
    {
        [SerializeField] private ItemAsset[] items = null;

        public ItemAsset GetItem(int level)
        {
            return level >= this.items.Length ? null : this.items[level];
        }
    }
}