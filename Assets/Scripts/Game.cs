using System;
using MiniIT.Test.Items;

namespace MiniIT.Test
{
    public static class Game
    {
        public static event Action<object, int> OnProfitEvent;
        
        private static int _level = 1;
        private static LevelManager _levelManager;

        public static LevelManager LevelManager => _levelManager;
        public static int Level => _level;
        
        public static void SetLevelManager(LevelManager levelManager)
        {
            _levelManager = levelManager;
        }

        public static void SetLevel(int newLevel)
        {
            _level = newLevel;
        }

        public static void MakeProfit(Item sender, int profit)
        {
            OnProfitEvent?.Invoke(sender, profit);
        }
    }
}