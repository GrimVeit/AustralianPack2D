using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class HelperLevelScoreBalance
{
    public static LevelScoreConfig GetConfig(GameLevel level)
    {
        return level switch
        {
            GameLevel.Level1_4 => new LevelScoreConfig { TotalPairs = 2, StartGift = CardBoxType.None, PriorityMoveThreshold = -1, StandardMoveThreshold = -1 },
            GameLevel.Level2_8 => new LevelScoreConfig { TotalPairs = 4, StartGift = CardBoxType.None, PriorityMoveThreshold = -1, StandardMoveThreshold = -1 },
            GameLevel.Level3_16 => new LevelScoreConfig { TotalPairs = 8, StartGift = CardBoxType.Standard, PriorityMoveThreshold = -1, StandardMoveThreshold = 18 },
            GameLevel.Level4_32 => new LevelScoreConfig { TotalPairs = 16, StartGift = CardBoxType.Priority, PriorityMoveThreshold = 45, StandardMoveThreshold = 60 },
            GameLevel.Level5_64 => new LevelScoreConfig { TotalPairs = 32, StartGift = CardBoxType.Priority, PriorityMoveThreshold = 110 , StandardMoveThreshold = 150 },
            _ => new LevelScoreConfig { TotalPairs = 2, StartGift = CardBoxType.None, PriorityMoveThreshold = -1, StandardMoveThreshold = -1 }
        };
    }
}

public class LevelScoreConfig
{
    public int TotalPairs;
    public CardBoxType StartGift;
    public int PriorityMoveThreshold;
    public int StandardMoveThreshold;
}
