using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseChickenModel
{
    private List<ChickenType> chickenTypes = new();

    private ChickenType _currentType = ChickenType.None;

    private readonly IStoreChickenListener _storeChickenListener;

    private readonly ISoundProvider _soundProvider;

    public ChooseChickenModel(IStoreChickenListener storeChickenListener, ISoundProvider soundProvider)
    {
        _storeChickenListener = storeChickenListener;
        _soundProvider = soundProvider;
    }

    public void Initialize()
    {
        _storeChickenListener.OnChooseChickens += SetTypes;
    }

    public void Dispose()
    {
        _storeChickenListener.OnChooseChickens -= SetTypes;
    }

    public void SetOpenSound()
    {
        _soundProvider.PlayOneShot("SpawnChicken");
    }

    public void Choose(ChickenType chickenType)
    {
        if(_currentType == chickenType) return;

        OnUnchoose?.Invoke(_currentType);

        _currentType = chickenType;

        for (int i = 0; i < chickenTypes.Count; i++)
        {
            if(_currentType != chickenTypes[i])
            {
                OnUnchoose?.Invoke(chickenTypes[i]);
            }
            else
            {
                _soundProvider.PlayOneShot("ChooseChicken");

                OnChoose?.Invoke(chickenTypes[i]);
            }
        }
    }

    private void SetTypes(List<ChickenType> types)
    {
        _currentType = ChickenType.None;

        chickenTypes = new List<ChickenType>(types);

        OnSetTypes?.Invoke(chickenTypes);

        chickenTypes.ForEach(data => OnChoose?.Invoke(data));
    }

    #region Output

    public event Action<List<ChickenType>> OnSetTypes;

    public event Action<ChickenType> OnChoose;
    public event Action<ChickenType> OnUnchoose;

    #endregion
}
