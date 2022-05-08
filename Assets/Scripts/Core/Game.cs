using System;
using Rodser.MergeTwins.Items;

namespace Rodser.MergeTwins
{
    public static class Game
    {
        public static event Action<object, int> OnProfitEvent;
        public static event Action<object> OnVictoryEvent;
        public static event Action<object> OnRiasingLevelEvent;
        public static event Action OnGameOverEvent;

        public static GameManager GameManager { get; set; } = null;
        public static AudioManager AudioManager { get; set; } = null;
        public static bool IsPlaying { get; set; } = false;

        public static void StartGame()
        {
            IsPlaying = true;
            GameManager.StartLevel();
        }

        public static void MakeProfit(Item sender, int profit)
        {
            OnProfitEvent?.Invoke(sender, profit);
        }

        public static void GameOver()
        {
            OnGameOverEvent?.Invoke();
            IsPlaying = false;
        }

        public static void OnClickButton(object sender)
        {
            AudioManager.OnClickSound(sender);
        }

        internal static void Victory(object sender)
        {
            OnVictoryEvent?.Invoke(sender);
        }

        internal static void RiasingLevel(object sender)
        {
            OnRiasingLevelEvent?.Invoke(sender);
        }
    }
}