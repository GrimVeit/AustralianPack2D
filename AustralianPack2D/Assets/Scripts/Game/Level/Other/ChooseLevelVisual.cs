using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ChooseLevelVisual : MonoBehaviour
{
    public GameLevel GameLevel => gameLevel;

    [SerializeField] private GameLevel gameLevel;
    [SerializeField] private Button buttonLevel;
    [SerializeField] private Image imageLevel;

    private Tween tweenColor;

    public void Initialize()
    {
        buttonLevel.onClick.AddListener(Choose);
    }

    public void Dispose()
    {
        buttonLevel.onClick.RemoveListener(Choose);
    }

    #region Input

    public void Select(Color colorSelect, float time)
    {
        tweenColor?.Kill();

        tweenColor = imageLevel.DOColor(colorSelect, time);
    }

    public void Deselect(Color colorDeselect, float time)
    {
        tweenColor?.Kill();

        tweenColor = imageLevel.DOColor(colorDeselect, time);
    }

    #endregion

    #region Output

    public event Action<GameLevel> OnChoose;

    private void Choose()
    {
        OnChoose?.Invoke(gameLevel);
    }

    #endregion
}
