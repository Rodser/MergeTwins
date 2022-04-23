using UnityEngine;

namespace MiniIT.Test.Items
{
    [CreateAssetMenu(fileName ="Item", menuName ="Game/Item", order = 2)]
    public class ItemAsset : ScriptableObject
    {
        [SerializeField] private int level = 1;
        [SerializeField] private int profit = 10;
        [SerializeField] private Item prefab = null;

        private Item currentItem = null;
        
        public int Level => level;
        public int Profit => profit;

        public void Spawn(Cell parentCell, Vector3 position)
        {
            this.currentItem = Instantiate(this.prefab, position, Quaternion.identity);
            this.currentItem.SetParent(this, parentCell);
        }
    }
}
