using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FullPicturePanel_Game : MovePanel
{
    [SerializeField] private ScaleEffect _scaleEffect;
    [SerializeField] private Button buttonExit;

    public override void Initialize()
    {
        base.Initialize();

        _scaleEffect.Initialize();

        buttonExit.onClick.AddListener(ClickToExit);
    }

    public override void Dispose()
    {
        base.Dispose();

        _scaleEffect.Dispose();

        buttonExit.onClick.RemoveListener(ClickToExit);
    }

    public override void ActivatePanel()
    {
        base.ActivatePanel();

        _scaleEffect.ActivateEffect();
    }

    public override void DeactivatePanel()
    {
        base.DeactivatePanel();

        _scaleEffect.DeactivateEffect();
    }

    #region Output

    public event Action OnClickExit;

    private void ClickToExit()
    {
        OnClickExit?.Invoke();
    }

    #endregion
}
