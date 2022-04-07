using UnityEngine;

namespace MiniIT.Test.Items
{
    public class ItemView : MonoBehaviour, IItem
    {
        private Vector3 startPosition;
        private Vector3 offset;
        private Vector3 coord;

        private void OnMouseDown()
        {
            startPosition = transform.position;
            coord = Camera.main.WorldToScreenPoint(transform.position);
            offset = transform.position - GetMouseWorldPosition();
        }

        private void OnMouseDrag()
        {
            transform.position = GetMouseWorldPosition() + offset;
            Debug.Log($"x = {transform.position.x}, y = {transform.position.y}, z = {transform.position.z}");
        }

        private void OnMouseUp()
        {
            transform.position = startPosition;
        }

        private Vector3 GetMouseWorldPosition()
        {
            Vector3 mousePoint = Input.mousePosition;
            mousePoint.z = coord.z;
            Debug.Log($"x = {mousePoint.x}, y = {mousePoint.y}, z = {mousePoint.z}");
            return Camera.main.ScreenToWorldPoint(mousePoint);
        }
    }
}