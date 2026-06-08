using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LosePanel_Game : MovePanel
{
    [SerializeField] private UIEffectCombination combination;
    [SerializeField] private Button buttonRestart;
    [SerializeField] private Button buttonExit;
    [SerializeField] private Button buttonMenu;

    public override void Initialize()
    {
        base.Initialize();

        combination.Initialize();

        buttonRestart.onClick.AddListener(ClickRestart);
        buttonMenu.onClick.AddListener(ClickMenu);
        buttonExit.onClick.AddListener(ClickExit);
    }

    public override void Dispose()
    {
        base.Dispose();

        combination.Dispose();

        buttonRestart.onClick.RemoveListener(ClickRestart);
        buttonMenu.onClick.RemoveListener(ClickMenu);
        buttonExit.onClick.RemoveListener(ClickExit);
    }

    public override void ActivatePanel()
    {
        base.ActivatePanel();

        combination.ActivateEffect();
    }

    public override void DeactivatePanel()
    {
        base.DeactivatePanel();

        combination.DeactivateEffect();
    }

    #region Output

    public event Action OnClickToRestart;
    public event Action OnClickToExit;
    public event Action OnClickToMenu;

    private void ClickRestart()
    {
        OnClickToRestart?.Invoke();
    }

    private void ClickExit()
    {
        OnClickToExit?.Invoke();
    }

    private void ClickMenu()
    {
        OnClickToMenu?.Invoke();
    }

    #endregion
}
