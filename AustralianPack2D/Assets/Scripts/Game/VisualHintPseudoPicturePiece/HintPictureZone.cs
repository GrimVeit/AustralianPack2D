using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HintPictureZone : MonoBehaviour, IHintPictureZone
{
    public void SetType(ChickenType type)
    {
        OnSetType?.Invoke(type);
    }

    #region Output

    public event Action<ChickenType> OnSetType;

    #endregion
}
