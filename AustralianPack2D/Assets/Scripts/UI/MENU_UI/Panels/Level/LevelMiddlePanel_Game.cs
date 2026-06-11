using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelMiddlePanel_Game : MovePanel
{
    [SerializeField] private UIEffectCombination effectCombination;
    [SerializeField] private Button buttonLevel1;
    [SerializeField] private Button buttonLevel2;
    [SerializeField] private Button buttonLevel3;
    [SerializeField] private Button buttonLevel4;
    [SerializeField] private Button buttonLevel5;

    public override void Initialize()
    {
        base.Initialize();

        buttonLevel1.onClick.AddListener(ClickLevel1);
        buttonLevel2.onClick.AddListener(ClickLevel2);
        buttonLevel3.onClick.AddListener(ClickLevel3);
        buttonLevel4.onClick.AddListener(ClickLevel4);
        buttonLevel5.onClick.AddListener(ClickLevel5);

        effectCombination.Initialize();

    }

    public override void Dispose()
    {
        base.Dispose();

        buttonLevel1.onClick.RemoveListener(ClickLevel1);
        buttonLevel2.onClick.RemoveListener(ClickLevel2);
        buttonLevel3.onClick.RemoveListener(ClickLevel3);
        buttonLevel4.onClick.RemoveListener(ClickLevel4);
        buttonLevel5.onClick.RemoveListener(ClickLevel5);

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

    public event Action OnClickLevel1;
    public event Action OnClickLevel2;
    public event Action OnClickLevel3;
    public event Action OnClickLevel4;
    public event Action OnClickLevel5;

    private void ClickLevel1()
    {
        OnClickLevel1?.Invoke();
    }

    private void ClickLevel2()
    {
        OnClickLevel2?.Invoke();
    }

    private void ClickLevel3()
    {
        OnClickLevel3?.Invoke();
    }

    private void ClickLevel4()
    {
        OnClickLevel4?.Invoke();
    }

    private void ClickLevel5()
    {
        OnClickLevel5?.Invoke();
    }

    #endregion
}
