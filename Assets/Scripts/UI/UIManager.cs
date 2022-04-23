using UnityEngine;
using UnityEngine.UI;

namespace MiniIT.Test.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Menu menu = null;
        [SerializeField] private CoinUI coinUI = null;
        [SerializeField] private GameOverUI gameOverUI = null;
        [SerializeField] private Button buttonMenu = null;
        [SerializeField] private AudioSource clickSound = null;
        
        public Menu Menu => menu;
        public CoinUI CoinUI => coinUI;

        private void Start()
        {
            this.buttonMenu.onClick.AddListener(GetMenu);
        }

        private void OnEnable()
        {
            Game.OnClickButtonEvent += OnClickSound;
            Game.OnGameOverEvent += GameOver;
        }

        private void OnDisable()
        {
            Game.OnClickButtonEvent -= OnClickSound;
            Game.OnGameOverEvent -= GameOver;
        }

        private void GetMenu()
        {
            OnClickSound(this);
            this.menu.gameObject.SetActive(true);
            Time.timeScale = 0;
        }

        private void GameOver()
        {
            OnClickSound(this);
            this.gameOverUI.gameObject.SetActive(true);
            this.buttonMenu.gameObject.SetActive(false);
            Time.timeScale = 0;
        }

        private void OnClickSound(object sender)
        {
            this.clickSound.Play();
        }
    }
}