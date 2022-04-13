using System;
using MiniIT.Test.Grounds;
using MiniIT.Test.Items;
using UnityEngine;

namespace MiniIT.Test
{
    [CreateAssetMenu(fileName = "Cell", menuName = "Game/Cell", order = 0)]
    public class Cell : ScriptableObject
    {
        [SerializeField] private GroundAsset groundAsset = null;

        private ItemAsset itemAsset = null;
        private Ground ground = null;
        private Vector3 position;
        private bool isFree = true;

        public bool IsFree => isFree;
        public int Number { get; set; }
        public Vector3 Position => position;
        public Ground Ground => ground;

        public void Initialization(Vector3 position, Transform parent)
        {
            this.position = position;
            this.ground = Instantiate(this.groundAsset.Prefab, position, Quaternion.identity, parent);
        }

        public Cell CloneCell()
        {
            Cell newCell = ScriptableObject.CreateInstance<Cell>();
            newCell.groundAsset = this.groundAsset;
            return newCell;
        }

        public void BecomeFree()
        {
            this.isFree = true;
            this.itemAsset = null;
        }

        internal void SpawnItem(ItemAsset itemAsset)
        {
            this.itemAsset = itemAsset;
            this.itemAsset.Spawn(this, this.Position);
            this.isFree = false;
        }
    }
}