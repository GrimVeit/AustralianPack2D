using System;
using System.Collections.Generic;
using UnityEngine;

public class SpawnerChickenModel
{
    private readonly IStoreChickenListener _storeChickenListener;
    private readonly ISoundProvider _soundProvider;

    private  List<ChickenType> _chickenTypes = new();

    public SpawnerChickenModel(IStoreChickenListener storeChickenListener, ISoundProvider soundProvider)
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

    public void SpawnChickens()
    {
        OnSpawnChicken?.Invoke(_chickenTypes, _soundProvider);
    }

    public void SpawnSound()
    {
        //_soundProvider.PlayOneShot("SpawnChicken");
    }

    private void SetTypes(List<ChickenType> types)
    {
        _chickenTypes = types;
    }

    #region Output

    public event Action<List<ChickenType>, ISoundProvider> OnSpawnChicken;

    #endregion
}
