using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopOpenPackFooterPanel_Menu : MovePanel
{
    [SerializeField] private UIEffectCombination effectCombination;
    [SerializeField] private Button buttonAlbum;

    public override void Initialize()
    {
        base.Initialize();

        effectCombination.Initialize();

        buttonAlbum.onClick.AddListener(ClickAlbum);
    }

    public override void Dispose()
    {
        base.Dispose();

        effectCombination.Dispose();

        buttonAlbum.onClick.RemoveListener(ClickAlbum);
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

    public event Action OnClickToAlbum;

    private void ClickAlbum()
    {
        OnClickToAlbum?.Invoke();
    }

    #endregion
}
