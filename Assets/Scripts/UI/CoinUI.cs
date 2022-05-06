using TMPro;
using UnityEngine;

namespace Rodser.MergeTwins.UI
{
    public class CoinUI : MonoBehaviour, ISaveable
    {
        [SerializeField] private TextMeshProUGUI textCoin = null;

        private int coin = 0;
        
        private void Start()
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
            this.coin += profit;
            this.textCoin.text = this.coin.ToString();
        }

        public void Save()
        {
            PlayerPrefs.SetInt("coin", coin);
        }

        public void Load()
        {
            coin = PlayerPrefs.GetInt("coin");
        }
    }
}