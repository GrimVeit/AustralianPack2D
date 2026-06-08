using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardsHeaderPanel_Game : MovePanel
{
    [SerializeField] private UIEffectCombination combination;
    [SerializeField] private Button buttonExit;
    [SerializeField] private Button buttonMenu;

    public override void Initialize()
    {
        base.Initialize();

        combination.Initialize();

        buttonExit.onClick.AddListener(ClickToExit);
        buttonMenu.onClick.AddListener(ClickToMenu);
    }

    public override void Dispose()
    {
        base.Dispose();

        combination.Dispose();

        buttonExit.onClick.RemoveListener(ClickToExit);
        buttonMenu.onClick.RemoveListener(ClickToMenu);
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

    public event Action OnClickToExit;
    public event Action OnClickToMenu;

    private void ClickToExit()
    {
        OnClickToExit?.Invoke();
    }

    private void ClickToMenu()
    {
        OnClickToMenu?.Invoke();
    }

    #endregion
}
