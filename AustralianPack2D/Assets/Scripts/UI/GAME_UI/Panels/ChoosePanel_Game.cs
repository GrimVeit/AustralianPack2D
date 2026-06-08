using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChoosePanel_Game : MovePanel
{
    [SerializeField] private UIEffectCombination combination;
    [SerializeField] private Button buttonChoose;

    public override void Initialize()
    {
        base.Initialize();

        combination.Initialize();

        buttonChoose.onClick.AddListener(Chooose);
    }

    public override void Dispose()
    {
        base.Dispose();

        combination.Dispose();

        buttonChoose.onClick.RemoveListener(Chooose);
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

    public event Action OnChoose;

    private void Chooose()
    {
        OnChoose?.Invoke();
    }

    #endregion
}
