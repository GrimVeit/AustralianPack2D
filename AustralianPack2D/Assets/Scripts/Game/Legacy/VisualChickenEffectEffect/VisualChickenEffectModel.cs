using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualChickenEffectModel
{
    private readonly IStoreChickenListener _storeChickenListener;
    private readonly IChooseChickenListener _chooseChickenListener;

    private List<ChickenType> chickenTypes = new();

    public VisualChickenEffectModel(IChooseChickenListener chooseChickenListener, IStoreChickenListener storeChickenListener)
    {
        _chooseChickenListener = chooseChickenListener;
        _storeChickenListener = storeChickenListener;

        _storeChickenListener.OnChooseChickens += SetChickenTypes;
        _chooseChickenListener.OnChoose += Choose;
    }

    public void Initialize()
    {

    }

    public void Dispose()
    {
        _storeChickenListener.OnChooseChickens -= SetChickenTypes;
        _chooseChickenListener.OnChoose -= Choose;
    }

    private void Choose(ChickenType chickenType)
    {
        if(chickenTypes.Count == 0) return;

        var index = chickenTypes.IndexOf(chickenType);

        OnChoose?.Invoke(index);
    }

    private void SetChickenTypes(List<ChickenType> types)
    {
        chickenTypes = new List<ChickenType>(types);
    }

    #region Output

    public event Action<int> OnChoose;

    #endregion
}
