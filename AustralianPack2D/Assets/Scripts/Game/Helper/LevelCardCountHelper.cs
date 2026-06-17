public static class LevelCardCountHelper
{
    public static int GetCardCount(GameLevel level)
    {
        return level switch
        {
            GameLevel.Level1_4 => 4,
            GameLevel.Level2_8 => 8,
            GameLevel.Level3_16 => 16,
            GameLevel.Level4_32 => 32,
            GameLevel.Level5_64 => 64,
            _ => 4
        };
    }
}
