using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Unity.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayToolbarView : View
{
    [SerializeField] private Button buttonToolbar;
    [SerializeField] private Button buttonRestart;
    [SerializeField] private Button buttonExit;

    [Header("ARROW")]
    [SerializeField] private Transform transformArrow;
    [SerializeField] private Vector3 vectorArrowShow;
    [SerializeField] private Vector3 vectorArrowHide;
    [SerializeField, ReadOnly] private float timeArrow = 0.2f;

    [Header("TOOLBAR")]
    [SerializeField] private Transform transformToobar;
    [SerializeField] private Transform transformUp;
    [SerializeField] private Transform transformDown;
    [SerializeField] private float timeToolbar = 0.2f;

    [Header("BUTTONS")]
    [SerializeField] private UIEffectCombination effectCombination;

    private Tween tweenArrow;
    private Tween tweenToolbar;

    public void Initialize()
    {
        buttonToolbar.onClick.AddListener(ClickToToolbar);
        buttonRestart.onClick.AddListener(ClickToRestart);
        buttonExit.onClick.AddListener(ClickToExit);

        effectCombination.Initialize();
    }

    public void Dispose()
    {
        buttonToolbar.onClick.RemoveListener(ClickToToolbar);
        buttonRestart.onClick.RemoveListener(ClickToRestart);
        buttonExit.onClick.RemoveListener(ClickToExit);

        effectCombination.Dispose();
    }

    public void ShowToolbar()
    {
        tweenArrow?.Kill();
        tweenToolbar?.Kill();

        tweenArrow = transformArrow.DOLocalRotate(vectorArrowShow, timeArrow);
        tweenToolbar = transformToobar.DOLocalMove(transformUp.localPosition, timeToolbar);

        effectCombination.ActivateEffect();
    }

    public void HideToolbar()
    {
        tweenArrow?.Kill();
        tweenToolbar?.Kill();

        tweenArrow = transformArrow.DOLocalRotate(vectorArrowHide, timeArrow);
        tweenToolbar = transformToobar.DOLocalMove(transformDown.localPosition, timeToolbar);

        effectCombination.DeactivateEffect();
    }

    #region Output

    public event Action OnClickToToolbar;
    public event Action OnClickToRestart;
    public event Action OnClickToExit;

    private void ClickToToolbar()
    {
        OnClickToToolbar?.Invoke();
    }

    private void ClickToRestart()
    {
        OnClickToRestart?.Invoke();
    }

    private void ClickToExit()
    {
        OnClickToExit?.Invoke();
    }

    #endregion
}
