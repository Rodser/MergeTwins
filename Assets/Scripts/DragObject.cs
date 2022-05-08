using UnityEngine;

namespace Rodser.MergeTwins
{
    public class DragObject : MonoBehaviour
    {
        [SerializeField] private Collider colliderDrag = null;
        [SerializeField] private Vector3 jumpHeight;

        private Vector3 startPosition;
        private Vector3 offset;
        private Vector3 positionScreen;

        public void DestroyDragObject()
        {
            Destroy(this);
        }

        private void OnMouseDown()
        {
            this.startPosition = this.transform.position;
            this.transform.position += jumpHeight;
            this.colliderDrag.isTrigger = true;
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
            this.colliderDrag.isTrigger = false;
        }

        private Vector3 GetMouseWorldPosition()
        {
            Vector3 mousePoint = Input.mousePosition;
            mousePoint.z = this.positionScreen.z;
            return Camera.main.ScreenToWorldPoint(mousePoint);
        }
    }
}