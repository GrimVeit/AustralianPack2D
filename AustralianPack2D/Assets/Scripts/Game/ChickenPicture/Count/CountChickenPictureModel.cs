using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountChickenPictureModel
{
    private readonly IStoreChickenPictureProvider _storeChickenPictureProvider;

    public CountChickenPictureModel(IStoreChickenPictureProvider storeChickenPictureProvider)
    {
        _storeChickenPictureProvider = storeChickenPictureProvider;
    }

    public void ShowCount()
    {
        var counts = _storeChickenPictureProvider.GetTypesStats();

        foreach (var type in counts)
        {
            OnShowCount?.Invoke(type.Type, type.CompletedPictures, type.TotalPictures);
        }
    }

    #region Output

    public event Action<ChickenType, int, int> OnShowCount;

    #endregion
}
