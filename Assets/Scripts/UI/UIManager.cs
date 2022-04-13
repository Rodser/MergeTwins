using UnityEngine;

namespace MiniIT.Test.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Menu menu;
        [SerializeField] private CoinUI coinUI;


        public Menu Menu => menu;
        public CoinUI CoinUI => coinUI;
    }
}