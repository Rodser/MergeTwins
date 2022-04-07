using UnityEngine;
using UnityEngine.EventSystems;

namespace MiniIT.Test.Items
{
    [CreateAssetMenu(fileName ="Item", menuName ="Game/Item", order = 2)]
    public class Item : ScriptableObject
    {
        [SerializeField] private int level = 1;
        [SerializeField] private ItemView prefab = null;

        public int Level => level;
        public ItemView Prefab => prefab;
        
        
        public void Spawn(Vector3 position)
        {
            Instantiate(Prefab, position, Quaternion.identity);
        }
    }
}
