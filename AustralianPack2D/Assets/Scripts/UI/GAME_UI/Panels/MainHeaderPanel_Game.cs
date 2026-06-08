using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainHeaderPanel_Game : MovePanel
{
    [SerializeField] private UIEffectCombination effectCombination;
    [SerializeField] private Button buttonMenu;

    public override void Initialize()
    {
        base.Initialize();

        effectCombination.Initialize();

        buttonMenu.onClick.AddListener(ClickToMenu);
    }

    public override void Dispose()
    {
        base.Dispose();

        effectCombination.Dispose();

        buttonMenu.onClick.RemoveListener(ClickToMenu);
    }

    public override void ActivatePanel()
    {
        base.ActivatePanel();

        effectCombination.ActivateEffect();
    }

    public override void DeactivatePanel()
    {
        base.DeactivatePanel();

        effectCombination.DeactivateEffect();
    }

    #region Output

    public event Action OnClickToMenu;

    private void ClickToMenu()
    {
        OnClickToMenu?.Invoke();
    }

    #endregion
}
