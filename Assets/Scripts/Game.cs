namespace MiniIT.Test
{
    public static class Game
    {
        public static LevelManager LevelManager { get; set; }

        public static void SetLevelManager(LevelManager levelManager)
        {
            LevelManager = levelManager;
        }
    }
}