using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardPresentationPanel_Game : MovePanel
{
    [SerializeField] private UIEffectCombination effectCombination;
    [SerializeField] private Button buttonBack;

    public override void Initialize()
    {
        base.Initialize();

        effectCombination.Initialize();

        buttonBack.onClick.AddListener(ClickBack);
    }

    public override void Dispose()
    {
        base.Dispose();

        effectCombination.Dispose();

        buttonBack.onClick.RemoveListener(ClickBack);
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

    public event Action OnClickToBack;

    private void ClickBack()
    {
        OnClickToBack?.Invoke();
    }

    #endregion
}
