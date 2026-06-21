using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardDesignBuyVisualModel
{
    private readonly IStoreCardDesignEventsProvider _storeCardDesignEventsProvider;
    private readonly IStoreCardDesignProvider _storeCardDesignProvider;
    private readonly IStoreCardDesignInfoProvider _storeCardDesignInfoProvider;
    private readonly IMoneyProvider _moneyProvider;
    private readonly ISoundProvider _soundProvider;

    private int _currentDesignIndex = -1;

    public CardDesignBuyVisualModel(IStoreCardDesignEventsProvider storeCardDesignEventsProvider, IStoreCardDesignProvider storeCardDesignProvider, IStoreCardDesignInfoProvider storeCardDesignInfoProvider, IMoneyProvider moneyProvider, ISoundProvider soundProvider)
    {
        _storeCardDesignEventsProvider = storeCardDesignEventsProvider;
        _storeCardDesignProvider = storeCardDesignProvider;
        _storeCardDesignInfoProvider = storeCardDesignInfoProvider;
        _moneyProvider = moneyProvider;

        _storeCardDesignEventsProvider.OnOpenDesign += Open;
        _storeCardDesignEventsProvider.OnCloseDesign += Close;
        _storeCardDesignEventsProvider.OnSelectDesign += Select;
        _storeCardDesignEventsProvider.OnDeselectDesign += Deselect;
        _soundProvider = soundProvider;
    }

    public void Initialize()
    {
        Debug.Log(_storeCardDesignInfoProvider.CardDesignIndex);

        ChooseDesign(_storeCardDesignInfoProvider.CardDesignIndex, false);
    }

    public void Dispose()
    {
        _storeCardDesignEventsProvider.OnOpenDesign -= Open;
        _storeCardDesignEventsProvider.OnCloseDesign -= Close;
        _storeCardDesignEventsProvider.OnSelectDesign -= Select;
        _storeCardDesignEventsProvider.OnDeselectDesign -= Deselect;
    }



    public void ChooseDesign(int id, bool isSoundActivate = true)
    {
        if (_currentDesignIndex == id) return;

        var dataDesign = _storeCardDesignInfoProvider.GetCardDesignData(id);

        if (!dataDesign.IsOpen) return;

        OnUnchoose?.Invoke(_currentDesignIndex);

        _currentDesignIndex = id;

        Debug.Log(_currentDesignIndex);

        if (!dataDesign.IsSelect) _storeCardDesignProvider.SelectDesign(id);

        if (isSoundActivate)
            _soundProvider.PlayOneShot("ChooseCardDesign");

        _currentDesignIndex = id;
        OnChoose?.Invoke(_currentDesignIndex);
    }

    public void BuyDesign(int id, int price)
    {
        var dataDesign = _storeCardDesignInfoProvider.GetCardDesignData(id);
        if (dataDesign.IsOpen) return;

        if (_moneyProvider.CanAfford(price))
        {
            _soundProvider.PlayOneShot("Money");

            _storeCardDesignProvider.OpenDesign(id, () => _storeCardDesignProvider.SelectDesign(id));
            _moneyProvider.SendMoney(-price);
        }
        else
        {
            Debug.Log("NOT MONEY FOR BUY");
        }

        ChooseDesign(id, false);
    }

    #region Output

    public event Action<int> OnOpen;
    public event Action<int> OnClose;
    public event Action<int> OnSelect;
    public event Action<int> OnDeselect;

    public event Action<int> OnChoose;
    public event Action<int> OnUnchoose;

    private void Open(int id)
    {
        OnOpen?.Invoke(id);
    }

    private void Close(int id)
    {
        OnClose?.Invoke(id);
    }

    private void Select(int id)
    {
        OnSelect?.Invoke(id);
    }

    private void Deselect(int id)
    {
        OnDeselect?.Invoke(id);
    }

    #endregion
}
