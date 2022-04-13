using TMPro;
using UnityEngine;

namespace MiniIT.Test.UI
{
    public class CoinUI : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textCoin;

        private int coin = 0;
        
        private void Start()
        {
            textCoin.text = coin.ToString();
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
            coin += profit;
            textCoin.text = coin.ToString();
        }
    }
}