using Rodser.MergeTwins.Grounds;
using UnityEngine;

namespace Rodser.MergeTwins.Items
{
    [CreateAssetMenu(fileName ="Item", menuName ="Game/Item", order = 2)]
    public class ItemAsset : ScriptableObject
    {
        [SerializeField] private int levelItem = 1;
        [SerializeField] private int profit = 10;
        [SerializeField] private Item prefab = null;

        private Item currentItem = null;
        
        public int LevelItem => levelItem;
        public int Profit => profit;

        public void Spawn(Ground parentGround, Vector3 position)
        {
            this.currentItem = Instantiate(this.prefab, position, Quaternion.identity);
            this.currentItem.SetParent(this, parentGround);
        }
    }
}
