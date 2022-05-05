using System;
using Rodser.MergeTwins.Items;

namespace Rodser.MergeTwins
{
    public static class Game
    {
        public static event Action<object, int> OnProfitEvent;
        public static event Action<object> OnClickButtonEvent;
        public static event Action<object> OnVictoryEvent;
        public static event Action<object> OnRiasingLevelEvent;
        public static event Action OnGameOverEvent;
        
        private static bool _isPlaying = false;
        private static GameManager _gameManager = null;

        public static GameManager GameManager { get => _gameManager; set => _gameManager = value; }
        public static bool IsPlaying { get => _isPlaying; set => _isPlaying = value; }

        public static void StartGame()
        {
            _isPlaying = true;
            _gameManager.StartLevel();
        }

        public static void MakeProfit(Item sender, int profit)
        {
            OnProfitEvent?.Invoke(sender, profit);
        }

        public static void GameOver()
        {
            OnGameOverEvent?.Invoke();
            _isPlaying = false;
        }

        public static void OnClickButton(object sender)
        {
            OnClickButtonEvent?.Invoke(sender);
        }

        internal static void Victory(object sender)
        {
            OnVictoryEvent?.Invoke(sender);
        }

        internal static void RiasingLevel(object sender)
        {
            StartGame();
            OnRiasingLevelEvent?.Invoke(sender);
        }
    }
}