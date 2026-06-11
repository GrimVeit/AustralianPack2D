using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlbumHeaderPanel_Menu : MovePanel
{
    [SerializeField] private UIEffectCombination effectCombination;
    [SerializeField] private Button buttonBack;

    public override void Initialize()
    {
        base.Initialize();

        buttonBack.onClick.AddListener(ClickBack);

        effectCombination.Initialize();
    }

    public override void Dispose()
    {
        base.Dispose();

        buttonBack.onClick.RemoveListener(ClickBack);

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

    public event Action OnClickBack;

    private void ClickBack()
    {
        OnClickBack?.Invoke();
    }

    #endregion
}
