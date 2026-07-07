using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopChoosePackPanel_Menu : MovePanel
{

    [SerializeField] private UIEffectCombination effectCombination;
    [SerializeField] private Button buttonStandard;
    [SerializeField] private Button buttonPriority;

    public override void Initialize()
    {
        base.Initialize();

        buttonStandard.onClick.AddListener(ClickStandard);
        buttonPriority.onClick.AddListener(ClickPriority);

        effectCombination.Initialize();
    }

    public override void Dispose()
    {
        base.Dispose();

        buttonStandard.onClick.RemoveListener(ClickStandard);
        buttonPriority.onClick.RemoveListener(ClickPriority);

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

    public event Action OnClickStandard;
    public event Action OnClickPriority;

    private void ClickStandard()
    {
        OnClickStandard?.Invoke();
    }

    private void ClickPriority()
    {
        OnClickPriority?.Invoke();
    }

    #endregion
}
