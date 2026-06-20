using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlbumMiddlePanel_Menu : MovePanel
{
    [SerializeField] private UIEffectCombination effectCombination;
    [SerializeField] private Button buttonCommon;
    [SerializeField] private Button buttonUncommon;
    [SerializeField] private Button buttonRare;
    [SerializeField] private Button buttonEpic;
    [SerializeField] private Button buttonMythical;

    public override void Initialize()
    {
        base.Initialize();

        effectCombination.Initialize();

        buttonCommon.onClick.AddListener(ClickCommon);
        buttonUncommon.onClick.AddListener(ClickUncommon);
        buttonRare.onClick.AddListener(ClickRare);
        buttonEpic.onClick.AddListener(ClickEpic);
        buttonMythical.onClick.AddListener(ClickMythical);
    }

    public override void Dispose()
    {
        base.Dispose();

        effectCombination.Dispose();

        buttonCommon.onClick.RemoveListener(ClickCommon);
        buttonUncommon.onClick.RemoveListener(ClickUncommon);
        buttonRare.onClick.RemoveListener(ClickRare);
        buttonEpic.onClick.RemoveListener(ClickEpic);
        buttonMythical.onClick.RemoveListener(ClickMythical);
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

    public event Action OnClickCommon;
    public event Action OnClickUncommon;
    public event Action OnClickRare;
    public event Action OnClickEpic;
    public event Action OnClickMythical;

    private void ClickCommon()
    {
        OnClickCommon?.Invoke();
    }

    private void ClickUncommon()
    {
        OnClickUncommon?.Invoke();
    }

    private void ClickRare()
    {
        OnClickRare?.Invoke();
    }

    private void ClickEpic()
    {
        OnClickEpic?.Invoke();
    }

    private void ClickMythical()
    {
        OnClickMythical?.Invoke();
    }

    #endregion
}
