using Rodser.MergeTwins.Grounds;
using UnityEngine;

namespace Rodser.MergeTwins
{
    [CreateAssetMenu(fileName = "Cell", menuName = "Game/Cell", order = 0)]
    public class CellAsset : ScriptableObject
    {
        [SerializeField] private Ground prefab = null;

        private Ground currentGround = null;

        public Ground CurrentGround => currentGround;

        public void Initialization(Vector3 position, Transform parent, int number)
        {
            this.currentGround = Instantiate(prefab, position, Quaternion.identity, parent);
            this.currentGround.SetData(position, number);
        }
    }
}