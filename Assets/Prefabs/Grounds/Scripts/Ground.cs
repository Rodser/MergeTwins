using Rodser.MergeTwins.Items;
using System;
using UnityEngine;

namespace Rodser.MergeTwins.Grounds
{
    public class Ground : MonoBehaviour
    {
        public event Action<object> OnRemoveEvent;

        [SerializeField] private ParticleSystem mergeEffect = null;

        private Vector3 position;
        private ItemAsset itemAsset = null;
        private bool isFree = true;

        public int Number { get; set; }
        public bool IsFree => isFree;
        public Vector3 Position => position;

        public void SetData(Vector3 position, int number)
        {
            this.position = position;
            this.Number = number;
        }

        internal void Remove()
        {
            OnRemoveEvent?.Invoke(this);
            Destroy(gameObject);
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

        public void PlayMergeEffect()
        {
            Game.AudioManager.OnMergeSound(this);
            this.mergeEffect.Play();
        }
    }
}