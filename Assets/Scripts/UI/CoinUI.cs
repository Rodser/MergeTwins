using TMPro;
using YG;
using UnityEngine;

namespace Rodser.MergeTwins.UI
{
    public class CoinUI : MonoBehaviour, ISaveable
    {
        [SerializeField] private TextMeshProUGUI textCoin = null;

        private int coin = 0;
        
        private void Start()
        {
            SetCoinText();
        }

        private void SetCoinText()
        {
            this.textCoin.text = this.coin.ToString();
        }

        private void OnEnable()
        {
            Game.OnProfitEvent += Profit;
        }

        private void OnDisable()
        {
            Game.OnProfitEvent -= Profit;
        }

        private void Profit(object sender, int profit)
        {
            this.coin += profit * Game.GameManager.GetMultiplier();
            this.textCoin.text = this.coin.ToString();
        }

        public void Save()
        {
            YandexGame.savesData.money = coin;
        }

        public void Load()
        {
            coin = YandexGame.savesData.money;
            SetCoinText();
        }
    }
}