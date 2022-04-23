using UnityEngine;

namespace MiniIT.Test.Items
{
    public class ItemSpinning : MonoBehaviour
    {
        [SerializeField] private Vector3 angleRotate;
        [SerializeField] private float speedRotate = 1;

        private void FixedUpdate()
        {
            this.transform.Rotate(this.angleRotate * this.speedRotate);
        }
    }
}