using UnityEngine;

namespace Rodser.MergeTwins.Grounds
{
    [CreateAssetMenu(fileName = "Ground", menuName = "Game/Ground",order = 1)]
    public class GroundAsset : ScriptableObject
    {
        [SerializeField] private Ground prefab = null;

        public Ground Prefab => prefab;
    }
}
