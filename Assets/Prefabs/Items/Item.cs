using UnityEngine;

namespace MiniIT.Test.Items
{
    [CreateAssetMenu(fileName ="Item", menuName ="Game/Item", order = 2)]
    public class Item : ScriptableObject
    {
        [SerializeField] private int level = 1;
        [SerializeField] private ItemView prefab = null;

        private ItemView currentItem = null;
        private Cell parentCell = null;
        
        public int Level => level;
        public Cell ParentCell => parentCell;
        public ItemView CurrentItem => currentItem;
        
        public void Spawn(Cell parentCell, Vector3 position)
        {
            this.parentCell = parentCell;
            this.currentItem = Instantiate(this.prefab, position, Quaternion.identity);
            this.currentItem.SetParent(parentCell);
        }
    }
}
