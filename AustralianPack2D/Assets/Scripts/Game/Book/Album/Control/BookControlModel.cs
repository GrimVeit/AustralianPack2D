using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BookControlModel
{
    private readonly IBookPageListener _bookPageListener;
    private readonly IBookPageProvider _bookPageProvider;

    public BookControlModel(IBookPageListener bookPageListener, IBookPageProvider bookPageProvider)
    {
        _bookPageListener = bookPageListener;

        _bookPageListener.OnCanMoveLeft += CanMoveLeft;
        _bookPageListener.OnCanMoveRight += CanMoveRight;
        _bookPageListener.OnCannotMoveLeft += CannotMoveLeft;
        _bookPageListener.OnCannotMoveRight += CannotMoveRight;
        _bookPageProvider = bookPageProvider;
    }

    public void Initialize()
    {

    }

    public void Dispose()
    {
        _bookPageListener.OnCanMoveLeft -= CanMoveLeft;
        _bookPageListener.OnCanMoveRight -= CanMoveRight;
        _bookPageListener.OnCannotMoveLeft -= CannotMoveLeft;
        _bookPageListener.OnCannotMoveRight -= CannotMoveRight;
    }

    private void CanMoveLeft()
    {
        OnShowLeft?.Invoke();
    }

    private void CanMoveRight()
    {
        OnShowRight?.Invoke();
    }

    private void CannotMoveLeft()
    {
        OnHideLeft?.Invoke();
    }

    private void CannotMoveRight()
    {
        OnHideRight?.Invoke();
    }

    #region Input

    public void MoveLeft()
    {
        _bookPageProvider.OpenPastPage();
    }

    public void MoveRight()
    {
        _bookPageProvider.OpenSecondPage();
    }

    #endregion

    #region Output

    public event Action OnShowLeft;
    public event Action OnShowRight;

    public event Action OnHideLeft;
    public event Action OnHideRight;

    #endregion
}
