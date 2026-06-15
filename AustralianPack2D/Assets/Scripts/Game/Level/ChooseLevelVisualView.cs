using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseLevelVisualView : View
{
    [SerializeField] private List<ChooseLevelVisual> chooseLevelVisuals = new();

    [Header("UNIT PROPERTIES")]
    [SerializeField] private Color colorSelect;
    [SerializeField] private Color colorDeselect;
    [SerializeField] private float timeSelectDeselect;

    private readonly Dictionary<GameLevel, ChooseLevelVisual> _visualMap = new();

    public void Initialize()
    {
        _visualMap.Clear();

        for (int i = 0; i < chooseLevelVisuals.Count; i++)
        {
            var visual = chooseLevelVisuals[i];

            visual.OnChoose += ChooseLevel;
            visual.Initialize();

            _visualMap[visual.GameLevel] = visual;
        }
    }

    public void Dispose()
    {
        for (int i = 0; i < chooseLevelVisuals.Count; i++)
        {
            var visual = chooseLevelVisuals[i];

            visual.OnChoose -= ChooseLevel;
            visual.Dispose();
        }

        _visualMap.Clear();
    }

    public void Select(GameLevel level)
    {
        if (!_visualMap.TryGetValue(level, out var visual))
        {
            Debug.LogError($"Not found ChooseLevelVisual with GameLevel = {level}");
            return;
        }

        visual.Select(colorSelect, timeSelectDeselect);
    }

    public void Deselect(GameLevel level)
    {
        if (!_visualMap.TryGetValue(level, out var visual))
        {
            Debug.LogError($"Not found ChooseLevelVisual with GameLevel = {level}");
            return;
        }

        visual.Deselect(colorDeselect, timeSelectDeselect);
    }

    #region Output

    public event Action<GameLevel> OnChooseLevel;

    private void ChooseLevel(GameLevel level)
    {
        OnChooseLevel?.Invoke(level);
    }

    #endregion
}
