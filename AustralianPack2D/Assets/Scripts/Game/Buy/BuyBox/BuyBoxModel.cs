using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuyBoxModel
{
    private readonly IChooseBuyBoxListener _chooseBuyBoxListener;

    public BuyBoxModel(IChooseBuyBoxListener chooseBuyBoxListener)
    {
        _chooseBuyBoxListener = chooseBuyBoxListener;

        _chooseBuyBoxListener.OnChoose += SetStartPosition;
    }

    public void Initialize()
    {

    }

    public void Dispose()
    {
        _chooseBuyBoxListener.OnChoose -= SetStartPosition;
    }

    private void SetStartPosition(int id)
    {
        OnSetStartPosition?.Invoke(id);
    }

    #region Output

    public event Action<int> OnSetStartPosition;

    #endregion
}
