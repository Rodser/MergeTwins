using UnityEngine;

namespace MiniIT.Test.Items
{
    public class ItemView : MonoBehaviour, IItem
    {
        private Cell parentCell = null;
        
        public void SetParent(Cell parent)
        {
            this.parentCell = parent;
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("trigger!");
            ItemView otheItemView = other.gameObject.GetComponent<ItemView>();
            if (otheItemView.parentCell.Item.Level == this.parentCell.Item.Level)
            {
                Merge(otheItemView);
            }
        }

        private void Merge(ItemView otheItemView)
        {
            Debug.Log("Merge start");
            Item newItem = Game.LevelManager.GetItem(this.parentCell.Item.Level);
            if (newItem != null)
            {
                this.parentCell.SpawnItem(newItem);
                DestroyOldItems(otheItemView);
            }
        }

        private void DestroyOldItems(ItemView otheItemView)
        {
            Destroy(otheItemView.gameObject);
            Destroy(gameObject);
        }

    }
}