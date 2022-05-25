using TMPro;
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
        [SerializeField] private NextUI nextUI = null;
        [SerializeField] private VictoryUI victoryUI = null;
        [Space(10)]
        [SerializeField] private TextMeshProUGUI levelText = null;
        [SerializeField] private Button clearLevelButton = null;
                
        public Menu Menu => menu;
        public CoinUI CoinUI => coinUI;

        private void Start()
        {
            SetStartConfig();
            this.menu.gameObject.SetActive(true);
            this.buttonMenu.gameObject.SetActive(false);
            this.buttonMenu.Button.onClick.AddListener(GetMenu);
            this.clearLevelButton.onClick.AddListener(ClearLevel);
        }

        private void ClearLevel()
        {
            Game.GameManager.ClearLevel();
        }

        private void OnEnable()
        {
            Game.OnGameOverEvent += GameOver;
            Game.OnRiasingLevelEvent += NextLevel;
            Game.OnVictoryEvent += Victory;
        }

        private void OnDisable()
        {
            Game.OnGameOverEvent -= GameOver;
            Game.OnRiasingLevelEvent -= NextLevel;
            Game.OnVictoryEvent -= Victory;
        }

        public void SetStartConfig()
        {
            this.menu.gameObject.SetActive(false);
            this.coinUI.gameObject.SetActive(true);
            this.gameOverUI.gameObject.SetActive(false);
            this.nextUI.gameObject.SetActive(false);
            this.victoryUI.gameObject.SetActive(false);
            this.buttonMenu.gameObject.SetActive(true);
        }

        public void SetLevelText(int index)
        {
            this.levelText.text = index.ToString();
        }

        private void NextLevel(object sender)
        {
            this.nextUI.gameObject.SetActive(true);
            this.buttonMenu.gameObject.SetActive(false);
            Time.timeScale = 0;
        }

        internal void Victory(object sender)
        {
            this.victoryUI.gameObject.SetActive(true);
            this.buttonMenu.gameObject.SetActive(false);
            Time.timeScale = 0;
        }

        private void GetMenu()
        {
            Game.OnClickButton(this);
            this.menu.gameObject.SetActive(true);
            this.menu.SetButtonMenu();
            this.buttonMenu.gameObject.SetActive(false);
            Time.timeScale = 0;
        }

        private void GameOver()
        {
            Game.OnClickButton(this);
            this.gameOverUI.gameObject.SetActive(true);
            this.buttonMenu.gameObject.SetActive(false);
            Time.timeScale = 0;
        }
    }
}