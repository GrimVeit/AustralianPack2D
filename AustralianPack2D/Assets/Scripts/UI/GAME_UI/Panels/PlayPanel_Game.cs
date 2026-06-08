using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayPanel_Game : MovePanel
{
    [SerializeField] private UIEffectCombination effectCombination;
    [SerializeField] private Button buttonCollection;
    [SerializeField] private Button buttonSettings;

    public override void Initialize()
    {
        base.Initialize();

        buttonCollection.onClick.AddListener(ClickCollection);
        buttonSettings.onClick.AddListener(ClickSettings);

        effectCombination.Initialize();
    }

    public override void Dispose()
    {
        base.Dispose();

        buttonCollection.onClick.RemoveListener(ClickCollection);
        buttonSettings.onClick.RemoveListener(ClickSettings);

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

    public event Action OnClickCollection;
    public event Action OnClickSettings;

    private void ClickCollection()
    {
        OnClickCollection?.Invoke();
    }

    private void ClickSettings()
    {
        OnClickSettings?.Invoke();
    }

    #endregion
}
