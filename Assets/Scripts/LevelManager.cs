using MiniIT.Test.Items;
using UnityEngine;

namespace MiniIT.Test
{
    [CreateAssetMenu(fileName = "LevelManager", menuName = "Game/LevelManager", order = 3)]
    public class LevelManager : ScriptableObject
    {
        [SerializeField] private float timeToDefeat = 5f;
        [SerializeField] private ItemAsset[] items = null;

        public float TimeToDefeat => timeToDefeat;
        
        public ItemAsset GetItem(int level)
        {
            return level >= this.items.Length ? null : this.items[level];
        }
    }
}