using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinPanel_Game : MovePanel
{
    [SerializeField] private UIEffectCombination combination;
    [SerializeField] private Button buttonRestart;
    [SerializeField] private Button buttonBuy;
    [SerializeField] private Button buttonMenu;
    [SerializeField] private Button buttonExit;

    public override void Initialize()
    {
        base.Initialize();

        combination.Initialize();

        buttonRestart.onClick.AddListener(ClickRestart);
        buttonBuy.onClick.AddListener(ClickBuy);
        buttonExit.onClick.AddListener(ClickExit);
        buttonMenu.onClick.AddListener(ClickMenu);
    }

    public override void Dispose()
    {
        base.Dispose();

        combination.Dispose();

        buttonRestart.onClick.RemoveListener(ClickRestart);
        buttonBuy.onClick.RemoveListener(ClickBuy);
        buttonExit.onClick.RemoveListener(ClickExit);
        buttonMenu.onClick.RemoveListener(ClickMenu);
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
    public event Action OnClickToBuy;
    public event Action OnClickToMenu;
    public event Action OnClickToExit;

    private void ClickRestart()
    {
        OnClickToRestart?.Invoke();
    }

    private void ClickBuy()
    {
        OnClickToBuy?.Invoke();
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
