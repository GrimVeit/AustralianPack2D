using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoreLevelModel
{
    private readonly string LEVEL_NUMBER_KEY;

    public GameLevel Level => _currentGameLevel;

    public event Action<GameLevel> OnChangeLevel;

    private GameLevel _currentGameLevel;

    public StoreLevelModel(string healthKey)
    {
        LEVEL_NUMBER_KEY = healthKey;
    }

    public void Initialize()
    {
        _currentGameLevel = LoadLevel();

        OnChangeLevel?.Invoke(_currentGameLevel);
    }

    public void Dispose()
    {
        SaveLevelSafe(_currentGameLevel);
        PlayerPrefs.Save();
    }

    public void SetLevel(GameLevel level)
    {
        if(_currentGameLevel == level) return;

        if (!Enum.IsDefined(typeof(GameLevel), level)) return;

        _currentGameLevel = level;
        SaveLevelSafe(_currentGameLevel);

        OnChangeLevel?.Invoke(_currentGameLevel);
    }

    private GameLevel LoadLevel()
    {
        if (!PlayerPrefs.HasKey(LEVEL_NUMBER_KEY)) return GameLevel.Level1_4;

        int rawValue = PlayerPrefs.GetInt(LEVEL_NUMBER_KEY, (int)GameLevel.Level1_4);

        if (Enum.IsDefined(typeof(GameLevel), rawValue)) return (GameLevel)rawValue;

        return GameLevel.Level1_4;
    }

    private void SaveLevelSafe(GameLevel level)
    {
        PlayerPrefs.SetInt(LEVEL_NUMBER_KEY, (int)level);
    }
}

public enum GameLevel
{
    None,
    Level1_4,
    Level2_8,
    Level3_16,
    Level4_32,
    Level5_64,
}
