using System;

public class ChooseLevelVisualModel
{
    private readonly IStoreLevelListener _storeLevelListener;
    private readonly IStoreLevelProvider _storeLevelProvider;

    private GameLevel _currentGameLevel = GameLevel.None;

    public ChooseLevelVisualModel(IStoreLevelListener storeLevelListener, IStoreLevelProvider storeLevelProvider)
    {
        _storeLevelListener = storeLevelListener;
        _storeLevelProvider = storeLevelProvider;

        _storeLevelListener.OnChangeLevel += LevelChange;
    }

    public void Initialize()
    {
        
    }

    public void Dispose()
    {
        _storeLevelListener.OnChangeLevel -= LevelChange;
    }

    #region Output

    public event Action<GameLevel> OnLevelSelect;
    public event Action<GameLevel> OnLevelDeselect;

    private void LevelChange(GameLevel level)
    {
        if(_currentGameLevel == level) return;

        if(_currentGameLevel != GameLevel.None)
           OnLevelDeselect?.Invoke(_currentGameLevel);

        _currentGameLevel = level;
        OnLevelSelect?.Invoke(_currentGameLevel);
    }

    #endregion

    #region Input

    public void SetLevel(GameLevel level)
    {
        _storeLevelProvider.SetLevel(level);
    }

    #endregion
}
