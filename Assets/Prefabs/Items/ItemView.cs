using UnityEngine;

namespace MiniIT.Test.Items
{
    public class ItemView : MonoBehaviour, IItem
    {
        [SerializeField] private Collider collider = null;

        private Cell parentCell = null;
        private Vector3 startPosition;
        private Vector3 offset;
        private Vector3 positionScreen;

        public Collider Collider => collider;
        
        public void SetParent(Cell parent)
        {
            this.parentCell = parent;
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("trigger!");
            ItemView otheItemView = other.gameObject.GetComponent<ItemView>();
            if (otheItemView.parentCell.Item.Level == parentCell.Item.Level)
            {
                Merge(otheItemView);
            }
        }

        private void Merge(ItemView otheItemView)
        {
            Debug.Log("Merge start");
            Item newItem = Game.LevelManager.GetItem(parentCell.Item.Level);
            otheItemView.parentCell.SpawnItem(newItem);
        }

        private void OnMouseDown()
        {
            this.startPosition = this.transform.position;
            this.positionScreen = Camera.main.WorldToScreenPoint(this.transform.position);
            this.offset = this.transform.position - GetMouseWorldPosition();
        }

        private void OnMouseDrag()
        {
            this.transform.position = GetMouseWorldPosition() + this.offset;
        }

        private void OnMouseUp()
        {
            this.transform.position = this.startPosition;
        }

        private Vector3 GetMouseWorldPosition()
        {
            Vector3 mousePoint = Input.mousePosition;
            mousePoint.z = this.positionScreen.z;
            return Camera.main.ScreenToWorldPoint(mousePoint);
        }
    }
}