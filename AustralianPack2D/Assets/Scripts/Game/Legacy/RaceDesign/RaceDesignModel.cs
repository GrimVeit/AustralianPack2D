using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaceDesignModel
{
    private readonly ISlotMachineListener _slotMachineListener;

    public RaceDesignModel(ISlotMachineListener slotMachineListener)
    {
        _slotMachineListener = slotMachineListener;
    }

    public void Initialize()
    {
        _slotMachineListener.OnGetLocation += SetLocation;
    }

    public void Dispose()
    {
        _slotMachineListener.OnGetLocation -= SetLocation;
    }

    private void SetLocation(int id)
    {
        OnSetLocation?.Invoke(id);
    }

    #region Output

    public event Action<int> OnSetLocation;

    #endregion
}
