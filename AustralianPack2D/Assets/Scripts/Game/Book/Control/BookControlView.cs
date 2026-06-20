using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BookControlView : View
{
    [Header("Buttons")]
    [SerializeField] private Button buttonLeft;
    [SerializeField] private Button buttonRight;

    [Header("Effects")]
    [SerializeField] private UIEffect effectLeft;
    [SerializeField] private UIEffect effectRight;

    public void Initialize()
    {
        buttonLeft.onClick.AddListener(ClickLeft);
        buttonRight.onClick.AddListener(ClickRight);

        effectLeft.Initialize();
        effectRight.Initialize();

        effectRight.ActivateEffect();
    }

    public void Dispose()
    {
        buttonLeft.onClick.RemoveListener(ClickLeft);
        buttonRight.onClick.RemoveListener(ClickRight);

        effectLeft.Dispose();
        effectRight.Dispose();
    }

    #region Input

    public void ShowLeft()
    {
        effectLeft.ActivateEffect();
    }

    public void ShowRight()
    {
        effectRight.ActivateEffect();
    }

    public void HideLeft()
    {
        effectLeft.DeactivateEffect();
    }

    public void HideRight()
    {
        effectRight.DeactivateEffect();
    }

    #endregion

    #region Output

    public event Action OnClickToLeft;
    public event Action OnClickToRight;

    private void ClickLeft()
    {
        OnClickToLeft?.Invoke();
    }

    private void ClickRight()
    {
        OnClickToRight?.Invoke();
    }

    #endregion
}
