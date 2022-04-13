using System.Collections;
using UnityEngine;

namespace MiniIT.Test.Items
{
    public class Item : MonoBehaviour, IItem
    {
        [SerializeField] private Collider collider = null;
        [SerializeField] private Animator animator = null;
        
        [Header("Parameters of merge")]
        [SerializeField] private float timeOfMerge = 0.1f;
        [SerializeField] private float speedMove = 0.2f;

        private static readonly int isMerge = Animator.StringToHash("IsMerge");
        
        private ItemAsset parent = null;
        private Cell parentCell;
        private float startTime = 0f;
        private float journeyLength = 0f;
        private Vector3 otherPosition;

        public Collider Collider => collider;
        public Animator Animator => animator;
        public bool HasMerged { get; set; } = false;

        private void Update()
        {
            if (HasMerged)
            {
                this.Move();
            }
        }

        public void SetParent(ItemAsset parent, Cell parentCell)
        {
            this.parent = parent;
            this.parentCell = parentCell;
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("trigger!");
            Item otherItem = other.gameObject.GetComponent<Item>();
            
            if (otherItem == null || !otherItem.Collider.isTrigger)
            {
                return;
            }
            
            if (otherItem.parent.Level == this.parent.Level)
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
            this.parentCell.GroundAsset.Prefab.PlayMergeEffect();
            
            ItemAsset newItemAsset = Game.LevelManager.GetItem(this.parent.Level);
            StartCoroutine(this.ChangeItemRoutine(newItemAsset, otherItem));
        }

        private IEnumerator ChangeItemRoutine(ItemAsset newItemAsset, Item otherItem)
        {
            yield return new WaitForSeconds(this.timeOfMerge);
            if (newItemAsset != null)
            {
                this.parentCell.SpawnItem(newItemAsset);
            }
            this.DestroyItems(otherItem);
        }

        private void DestroyItems(Item otherItem)
        {
            otherItem.parentCell.BecomeFree();
            Destroy(otherItem.gameObject);
            Destroy(gameObject);
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
            this.otherPosition = otherItem.parentCell.Position;
            
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
            this.collider.enabled = false;
        }
    }
}