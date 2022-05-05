using UnityEngine;
using UnityEngine.UI;

namespace Rodser.MergeTwins.UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private Menu menu = null;
        [SerializeField] private CoinUI coinUI = null;
        [SerializeField] private GameOverUI gameOverUI = null;
        [SerializeField] private ButtonUI buttonMenu = null;

        [SerializeField] private AudioSource clickSound = null;
        
        public Menu Menu => menu;
        public CoinUI CoinUI => coinUI;

        private void Start()
        {
            this.buttonMenu.Button.onClick.AddListener(GetMenu);
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

        public void SetStartConfig()
        {
            this.menu.gameObject.SetActive(false);
            this.coinUI.gameObject.SetActive(true);
            this.gameOverUI.gameObject.SetActive(false);
            this.buttonMenu.gameObject.SetActive(true);
        }

        private void GetMenu()
        {
            OnClickSound(this);
            this.menu.gameObject.SetActive(true);
            this.menu.SetButtonMenu();
            this.buttonMenu.gameObject.SetActive(false);
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