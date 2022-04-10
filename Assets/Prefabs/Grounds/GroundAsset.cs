using UnityEngine;

namespace MiniIT.Test.Grounds
{
    [CreateAssetMenu(fileName = "Ground", menuName = "Game/Ground",order = 1)]
    public class GroundAsset : ScriptableObject, IGround
    {
        [SerializeField] private Ground prefab = null;

        public Ground Prefab => prefab;
    }
}
