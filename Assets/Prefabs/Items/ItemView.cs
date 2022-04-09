using System.Collections;
using UnityEngine;

namespace MiniIT.Test.Items
{
    public class ItemView : MonoBehaviour, IItem
    {
        [SerializeField] private Collider collider = null;
        [SerializeField] private Animator animator = null;
        
        [Header("Parameters of merge")]
        [SerializeField] private float speedMergeItems = 0.1f;
        [SerializeField] private float speedMove = 0.2f;

        private static readonly int isMerge = Animator.StringToHash("IsMerge");
        
        private Cell parentCell = null;
        private float startTime;
        private float journeyLength;
        private Vector3 otherPosition;

        public Collider Collider => collider;
        public Animator Animator => animator;
        public bool HasMerged { get; set; } = false;

        private void Update()
        {
            if (HasMerged)
            {
                float distCovered = (Time.time - startTime) * speedMove;
                float fractionOfJourney = distCovered / journeyLength;
                this.transform.position = Vector3.Lerp(this.transform.position, this.otherPosition, fractionOfJourney);
            }
        }


        public void SetParent(Cell parent)
        {
            this.parentCell = parent;
        }

        private void OnTriggerEnter(Collider other)
        {
            Debug.Log("trigger!");
            ItemView otherItemView = other.gameObject.GetComponent<ItemView>();
            
            if (otherItemView == null || !otherItemView.Collider.isTrigger)
            {
                return;
            }
            
            if (otherItemView.parentCell.Item.Level == this.parentCell.Item.Level)
            {
                otherItemView.GetComponent<DragObject>().DestroyDragObject();
                Merge(otherItemView);
            }
        }

        private void Merge(ItemView otherItemView)
        {
            ShutOffColliders(otherItemView);
            AnimateMerge(otherItemView);
            
            Item newItem = Game.LevelManager.GetItem(this.parentCell.Item.Level);
            if (newItem != null)
            {
                StartCoroutine(ChangeItemRoutine(newItem, otherItemView));
            }
            else
            {
                DestroyItems(otherItemView);
            }
        }

        private IEnumerator ChangeItemRoutine(Item newItem, ItemView otherItemView)
        {
            yield return new WaitForSeconds(this.speedMergeItems);
            otherItemView.parentCell.BecomeFree();
            Destroy(otherItemView.gameObject);
            this.parentCell.SpawnItem(newItem);
            Destroy(gameObject);
        }

        private void DestroyItems(ItemView otherItemView)
        {
            otherItemView.parentCell.BecomeFree();
            Destroy(otherItemView.gameObject);
            Destroy(gameObject);
        }

        private void AnimateMerge(ItemView otherItemView)
        {
            this.Animator.SetBool(isMerge, true);
            otherItemView.Animator.SetBool(isMerge, true);
            otherItemView.Move(this);
        }

        private void Move(ItemView otherItemView)
        {
            HasMerged = true;
            this.startTime = Time.time;
            this.journeyLength = Vector3.Distance(otherItemView.transform.position, this.transform.position);
            this.otherPosition = otherItemView.parentCell.Position;
            
        }

        private void ShutOffColliders(ItemView otherItemView)
        {
            otherItemView.Collider.enabled = false;
            this.collider.enabled = false;
        }
    }
}