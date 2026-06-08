using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class MainPanel_Game : MovePanel
{
    [SerializeField] private UIEffectCombination effectCombination;

    public override void Initialize()
    {
        base.Initialize();

        effectCombination.Initialize();
    }

    public override void Dispose()
    {
        base.Dispose();

        effectCombination.Dispose();
    }

    public override void ActivatePanel()
    {
        panel.SetActive(true);
        isActive = true;
        CanvasGroupAlpha(canvasGroup, 0, 1, time);

        effectCombination.ActivateEffect();
    }

    public override void DeactivatePanel()
    {
        isActive = false;
        CanvasGroupAlpha(canvasGroup, 1, 0, time, () => { panel.SetActive(false); });

        effectCombination.DeactivateEffect();
    }
}
