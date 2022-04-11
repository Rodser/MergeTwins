namespace MiniIT.Test
{
    public static class Game
    {
        private static int level = 1;
        
        public static LevelManager LevelManager { get; set; }
        public static int Level => level;
        
        public static void SetLevelManager(LevelManager levelManager)
        {
            LevelManager = levelManager;
        }

        public static void SetLevel(int newLevel)
        {
            level = newLevel;
        }
    }
}