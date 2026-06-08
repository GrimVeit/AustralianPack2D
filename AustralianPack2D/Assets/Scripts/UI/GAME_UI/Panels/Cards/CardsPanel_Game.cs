using System;
using UnityEngine;
using UnityEngine.UI;

public class CardsPanel_Game : MovePanel
{
    [SerializeField] private UIEffectCombination combination;

    public override void Initialize()
    {
        base.Initialize();

        combination.Initialize();
    }

    public override void Dispose()
    {
        base.Dispose();

        combination.Dispose();
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
}
