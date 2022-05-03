using System;
using Rodser.MergeTwins.Items;

namespace Rodser.MergeTwins
{
    public static class Game
    {
        public static event Action<object, int> OnProfitEvent;
        public static event Action<object> OnClickButtonEvent;
        public static event Action OnGameOverEvent;
        
        private static int _level = 1;
        private static bool _isPlaying = false;
        private static GameManager _gameManager = null;

        public static bool IsPlaying => _isPlaying;
        public static GameManager GameManager { get => _gameManager; set => _gameManager = value; }
        public static int Level { get => _level; set => _level = value; }

        public static void StartGame()
        {
            _isPlaying = true;
            _gameManager.StartLevel(_level);
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
    }
}