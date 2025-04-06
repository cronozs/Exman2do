using UnityEngine;
using TMPro;

namespace Shooter
{
    public class CoinManager : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI coinsText;
        public int coins;

        private void OnEnable()
        {
            Coin.onCoinCollect += AddCoins;
        }

        private void OnDisable()
        {
            Coin.onCoinCollect -= AddCoins;
        }

        private void AddCoins()
        {
            coins++;
            UpdateCanvas();
        }

        public void UpdateCanvas()
        {
            coinsText.text = coins.ToString();
        }
    }
}
