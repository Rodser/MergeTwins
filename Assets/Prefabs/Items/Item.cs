using UnityEngine;

namespace MiniIT.Test.Items
{
    [CreateAssetMenu(fileName ="Item", menuName ="Game/Item", order = 2)]
    public class Item : ScriptableObject, IItem
    {
        [SerializeField] private int level = 1;
        [SerializeField] private GameObject prefab = null;

        public int Level => level;
        public GameObject Prefab => prefab;
        
        
        public void Spawn(Vector3 position)
        {
            Instantiate(Prefab, position, Quaternion.identity);
        }
    }
}
