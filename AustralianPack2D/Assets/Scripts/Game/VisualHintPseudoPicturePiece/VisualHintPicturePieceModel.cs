using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualHintPicturePieceModel
{
    private readonly IPseudoPicturePieceListener _pseudoPicturePieceListener;

    public VisualHintPicturePieceModel(IPseudoPicturePieceListener pseudoPicturePieceListener)
    {
        _pseudoPicturePieceListener = pseudoPicturePieceListener;

        _pseudoPicturePieceListener.OnStartDrag += Show;
        _pseudoPicturePieceListener.OnStopDrag += Hide;
    }

    public void Initialize()
    {

    }

    public void Dispose()
    {
        _pseudoPicturePieceListener.OnStartDrag -= Show;
        _pseudoPicturePieceListener.OnStopDrag -= Hide;
    }

    private void Show()
    {
        OnShow?.Invoke();
    }

    private void Hide()
    {
        OnHide?.Invoke();
    }

    #region Output

    public event Action OnShow;
    public event Action OnHide;

    #endregion
}
