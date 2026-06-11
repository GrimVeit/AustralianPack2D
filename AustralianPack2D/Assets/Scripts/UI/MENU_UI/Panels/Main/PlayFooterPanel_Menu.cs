using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayFooterPanel_Menu : MovePanel
{
    [SerializeField] private UIEffectCombination effectCombination;
    [SerializeField] private Button buttonPlay;

    public override void Initialize()
    {
        base.Initialize();

        buttonPlay.onClick.AddListener(ClickPlay);

        effectCombination.Initialize();
    }

    public override void Dispose()
    {
        base.Dispose();

        buttonPlay.onClick.RemoveListener(ClickPlay);

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

    public event Action OnClickToPlay;

    private void ClickPlay()
    {
        OnClickToPlay?.Invoke();
    }

    #endregion
}
