using System;
using UnityEngine;
using UnityEngine.UI;

namespace MiniIT.Test.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Menu menu;
        [SerializeField] private CoinUI coinUI;
        [SerializeField] private Button buttonMenu;

        public Menu Menu => menu;
        public CoinUI CoinUI => coinUI;

        private void Start()
        {
            buttonMenu.onClick.AddListener(GetMenu);
        }

        private void GetMenu()
        {
            menu.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
    }
}