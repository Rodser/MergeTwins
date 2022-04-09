using UnityEngine;

namespace MiniIT.Test
{
    public class DragObject : MonoBehaviour
    {
        [SerializeField] private Collider collider = null;

        private Vector3 startPosition;
        private Vector3 offset;
        private Vector3 positionScreen;
        
        private void OnMouseDown()
        {
            this.startPosition = this.transform.position;
            this.collider.isTrigger = false;
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
            this.collider.isTrigger = true;
        }

        private Vector3 GetMouseWorldPosition()
        {
            Vector3 mousePoint = Input.mousePosition;
            mousePoint.z = this.positionScreen.z;
            return Camera.main.ScreenToWorldPoint(mousePoint);
        }
    }
}