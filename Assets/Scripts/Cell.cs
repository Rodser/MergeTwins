using MiniIT.Test.Grounds;
using MiniIT.Test.Items;
using UnityEngine;

namespace MiniIT.Test
{
    [CreateAssetMenu(fileName = "Cell", menuName = "Game/Cell", order = 0)]
    public class Cell : ScriptableObject
    {
        [SerializeField] private Ground ground = null;

        private Item item = null;
        private Vector3 position;
        private bool isFree = true;

        public bool IsFree => isFree;
        public int Number { get; set; }
        public Ground Ground => ground;
        public Item Item => item;

        public void Initialization(Vector3 position, Transform parent)
        {
            this.position = position;
            Instantiate(ground.Prefab, position, Quaternion.identity, parent);
        }

        public Cell CloneCell()
        {
            Cell newCell = ScriptableObject.CreateInstance<Cell>();
            newCell.ground = this.ground;
            return newCell;
        }

        internal void SpawnItem(Item item)
        {
            this.item = item;
            this.item.Spawn(this, this.position);
            this.isFree = false;
        }
    }
}