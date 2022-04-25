using System;
using MiniIT.Test.Items;

namespace MiniIT.Test
{
    public static class Game
    {
        public static event Action<object, int> OnProfitEvent;
        public static event Action<object> OnClickButtonEvent;
        public static event Action OnGameOverEvent;
        
        private static int _level = 1;
        private static LevelManager _levelManager;
        private static bool isPlaying = false;

        public static LevelManager LevelManager => _levelManager;
        public static int Level => _level;
        public static bool IsPlaying => isPlaying;
        
        public static void SetLevelManager(LevelManager levelManager)
        {
            _levelManager = levelManager;
        }

        public static void SetLevel(int newLevel)
        {
            _level = newLevel;
        }

        public static void StartGame()
        {
            isPlaying = true;
        }

        public static void MakeProfit(Item sender, int profit)
        {
            OnProfitEvent?.Invoke(sender, profit);
        }

        public static void GameOver()
        {
            OnGameOverEvent?.Invoke();
            isPlaying = false;
        }

        public static void OnClickButton(object sender)
        {
            OnClickButtonEvent?.Invoke(sender);
        }
    }
}