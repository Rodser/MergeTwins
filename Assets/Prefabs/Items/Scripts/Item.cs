using Rodser.MergeTwins.Grounds;
using System.Collections;
using UnityEngine;

namespace Rodser.MergeTwins.Items
{
    public class Item : MonoBehaviour
    {
        [SerializeField] private Collider colliderItem = null;
        [SerializeField] private Animator animator = null;
        
        [Header("Parameters of merge")]
        [SerializeField] private float timeOfMerge = 0.1f;
        [SerializeField] private float speedMove = 0.2f;

        private static readonly int isMerge = Animator.StringToHash("IsMerge");
        
        private ItemAsset parent = null;
        private Ground parentGround = null;
        private float startTime = 0f;
        private float journeyLength = 0f;
        private Vector3 otherPosition;

        public Collider Collider => colliderItem;
        public Animator Animator => animator;
        public bool HasMerged { get; set; } = false;

        private void Update()
        {
            if (HasMerged)
            {
                this.Move();
            }
        }

        public void SetParent(ItemAsset parent, Ground parentGround)
        {
            this.parent = parent;
            this.parentGround = parentGround;
        }

        private void OnTriggerEnter(Collider other)
        {
            Item otherItem = other.gameObject.GetComponent<Item>();
            
            if (otherItem == null || !Game.IsPlaying || !otherItem.Collider.isTrigger)
            {
                return;
            }
            
            if (otherItem.parent.LevelItem == this.parent.LevelItem)
            {
                otherItem.GetComponent<DragObject>().DestroyDragObject();
                this.Merge(otherItem);
                Game.MakeProfit(this, parent.Profit);
            }
        }

        private void Merge(Item otherItem)
        {
            this.ShutOffColliders(otherItem);
            this.AnimateMerge(otherItem);
            this.parentGround.PlayMergeEffect();

            ItemAsset newItemAsset = Game.GameManager.GetItem(this.parent.LevelItem);
            StartCoroutine(this.ChangeItemRoutine(newItemAsset, otherItem));
        }

        private IEnumerator ChangeItemRoutine(ItemAsset newItemAsset, Item otherItem)
        {
            yield return new WaitForSeconds(this.timeOfMerge);
            if (newItemAsset != null)
            {
                this.parentGround.SpawnItem(newItemAsset);
            }
            this.DestroyItems(otherItem);
        }

        private void DestroyItems(Item otherItem)
        {
            otherItem.parentGround.BecomeFree();
            Destroy(otherItem.gameObject);
            Destroy(this.gameObject);
        }

        private void AnimateMerge(Item otherItem)
        {
            this.Animator.SetBool(isMerge, true);
            otherItem.Animator.SetBool(isMerge, true);
            otherItem.BeginMove(this);
        }

        private void BeginMove(Item otherItem)
        {
            this.HasMerged = true;
            this.startTime = Time.time;
            this.journeyLength = Vector3.Distance(otherItem.transform.position, this.transform.position);
            this.otherPosition = otherItem.parentGround.Position;
            
        }

        private void Move()
        {
            float distCovered = (Time.time - startTime) * speedMove;
            float fractionOfJourney = distCovered / journeyLength;
            this.transform.position = Vector3.Lerp(this.transform.position, this.otherPosition, fractionOfJourney);
        }

        private void ShutOffColliders(Item otherItem)
        {
            otherItem.Collider.enabled = false;
            this.colliderItem.enabled = false;
        }
    }
}