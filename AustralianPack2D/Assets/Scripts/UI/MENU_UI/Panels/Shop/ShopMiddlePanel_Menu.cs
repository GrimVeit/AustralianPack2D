using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopMiddlePanel_Menu : MovePanel
{
    [SerializeField] private UIEffectCombination effectCombination;
    [SerializeField] private Button buttonCover;
    [SerializeField] private Button buttonCardPack;

    public override void Initialize()
    {
        base.Initialize();

        buttonCover.onClick.AddListener(ClickCover);
        buttonCardPack.onClick.AddListener(ClickCardPack);

        effectCombination.Initialize();

    }

    public override void Dispose()
    {
        base.Dispose();

        buttonCover.onClick.RemoveListener(ClickCover);
        buttonCardPack.onClick.RemoveListener(ClickCardPack);

        effectCombination.Dispose();
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

    public event Action OnClickCover;
    public event Action OnClickCardPack;

    private void ClickCover()
    {
        OnClickCover?.Invoke();
    }

    private void ClickCardPack()
    {
        OnClickCardPack?.Invoke();
    }

    #endregion
}
