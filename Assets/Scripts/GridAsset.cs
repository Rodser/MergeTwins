using UnityEngine;

namespace Rodser.MergeTwins
{
    [CreateAssetMenu(fileName ="Grid", menuName ="Game/Grid", order = 7)]
    public class GridAsset : ScriptableObject
    {
        [SerializeField] private float spaceBetweenCells = 1.1f;

        public float SpaceBetweenCells => spaceBetweenCells;
    }
}
