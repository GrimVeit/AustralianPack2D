using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinPanel_Game : MovePanel
{
    [SerializeField] private UIEffectCombination effectCombination;
    [SerializeField] private Button buttonMenu;
    [SerializeField] private Button buttonNewGame;

    public override void Initialize()
    {
        base.Initialize();

        effectCombination.Initialize();

        buttonMenu.onClick.AddListener(ClickMenu);
        buttonNewGame.onClick.AddListener(ClickGame);
    }

    public override void Dispose()
    {
        base.Dispose();

        effectCombination.Dispose();

        buttonMenu.onClick.RemoveListener(ClickMenu);
        buttonNewGame.onClick.RemoveListener(ClickGame);
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
    public event Action OnClickToGame;

    private void ClickMenu()
    {
        OnClickToMenu?.Invoke();
    }

    public void ClickGame()
    {
        OnClickToGame?.Invoke();
    }

    #endregion
}
