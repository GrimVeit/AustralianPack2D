using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HelperLevelScoreBalance
{
    public static LevelScoreConfig GetConfig(GameLevel level)
    {
        return level switch
        {
            GameLevel.Level1_4 => new LevelScoreConfig { TotalPairs = 2, GoodMoveThreshold = 2 },
            GameLevel.Level2_8 => new LevelScoreConfig { TotalPairs = 4, GoodMoveThreshold = 10 },
            GameLevel.Level3_16 => new LevelScoreConfig { TotalPairs = 8, GoodMoveThreshold = 18 },
            GameLevel.Level4_32 => new LevelScoreConfig { TotalPairs = 16, GoodMoveThreshold = 40 },
            GameLevel.Level5_64 => new LevelScoreConfig { TotalPairs = 32, GoodMoveThreshold = 100 },
            _ => new LevelScoreConfig { TotalPairs = 2, GoodMoveThreshold = 2 }
        };
    }
}

public struct LevelScoreConfig
{
    public int TotalPairs;
    public int GoodMoveThreshold;
}
