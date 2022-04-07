using UnityEngine;

namespace MiniIT.Test.Grounds
{
    [CreateAssetMenu(fileName = "Ground", menuName = "Game/Ground",order = 1)]
    public class Ground : ScriptableObject, IGround
    {
        [SerializeField] private GameObject prefab = null;

        public GameObject Prefab => prefab;
    }
}
